using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using EosSharp.Core.Api.v1;
using EosSharp.Core.Providers;
using GraphEosStreamer.SHiP;
using GraphEosStreamer.SHiP.Variants;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;


namespace GraphEosStreamer.BlockReader
{
    public class BlockReaderService : BackgroundService
    {
        private uint _headBlockNum;

        public static Abi _abi;

        private readonly Uri _uri;

        private readonly ClientWebSocket _clientWebSocket;

        private readonly AbiSerializationProvider _abiSerializationProvider;

        private readonly ChannelWriter<byte[]> _channel;

        private readonly Options _options;

        private readonly uint _startBlockNum;
        private readonly uint _endBlockNum;
        private readonly string _shipUrl;
        private uint _maxMessagesInFlight;

        public BlockReaderService(ChannelWriter<byte[]> channel,
            IOptionsMonitor<Options> optionsMonitor)
        {
            _options = optionsMonitor.CurrentValue;

            _startBlockNum = _options.StartBlockNum;
            _endBlockNum = _options.EndBlockNum;
            _shipUrl = _options.ShipUrl;
            _maxMessagesInFlight = _options.MaxMessagesInFlight;

            _headBlockNum = 0;
            _uri = new Uri(_shipUrl);
            _clientWebSocket = new ClientWebSocket();
            _abiSerializationProvider = new AbiSerializationProvider(null);
            _channel = channel;

            optionsMonitor.OnChange(OnOptionsChanged);
        }

        private void OnOptionsChanged(Options options)
        {
            _maxMessagesInFlight = options.MaxMessagesInFlight;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var connect = Connect(cancellationToken);
            await SendStatusRequest(new GetStatusRequestV0(), cancellationToken);
            await Task.Delay(1000, cancellationToken);
            
            var sendBlocksRequest = SendBlocksRequest(new GetBlocksRequestV0(_startBlockNum, _endBlockNum, _maxMessagesInFlight, new BlockPosition[] { }, false,
                true, true, true), cancellationToken);
            await Task.WhenAll(new Task[] { connect, sendBlocksRequest });

            //            var sendBlocksRequest = SendBlocksRequest(new GetBlocksRequestV0(_headBlockNum-10, UInt32.MaxValue, 10000, new BlockPosition[]{} ,false,
            //var sendBlocksRequest = SendBlocksRequest(new GetBlocksRequestV0(122462120, 122462125, 6, new BlockPosition[]{} ,false,
            //  true, true, true));
        }

        public async Task Connect(CancellationToken clt)
        {
            try
            {
                await _clientWebSocket.ConnectAsync(_uri, clt);
                Log.Information("WS opened");

                await Receive(clt);
            }
            catch (Exception ex)
            {
                Log.Error(ex,"");
            }
            finally
            {
                _clientWebSocket?.Dispose();
            }
        }

        public async Task Receive(CancellationToken clt)
        {
            while (_clientWebSocket.State == WebSocketState.Open)
            {
                var closeCode = WebSocketCloseStatus.Empty;

                var buffer = new ArraySegment<byte>(new byte[41943040]); // 40MB
                try
                {
                    while (_clientWebSocket.State == WebSocketState.Open)
                    {
                        await using (var ms = new MemoryStream())
                        {
                            WebSocketReceiveResult result = null;
                            do
                            {
                                result = await _clientWebSocket.ReceiveAsync(buffer, clt);
                                ms.Write(buffer.Array, buffer.Offset, result.Count); // don't write async!
                            } while (!result.EndOfMessage);

                            ms.Seek(0, SeekOrigin.Begin);

                            if (result.MessageType == WebSocketMessageType.Text)
                            {
                                Log.Information("Received Text");
                                using (var reader = new StreamReader(ms, Encoding.UTF8))
                                {
                                    var message = await reader.ReadToEndAsync();
                                    OnTextMessageReceived(message);
                                }
                            }
                            else if (result.MessageType == WebSocketMessageType.Binary)
                            {
                                await OnBlockReceived(ms.ToArray(), clt);
                            }
                            else if (result.MessageType == WebSocketMessageType.Close)
                            {
                                await Close(clt);
                                closeCode = result.CloseStatus.GetValueOrDefault(WebSocketCloseStatus.Empty);
                                break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e,"");
                    Log.Information("CloseCode:" + closeCode);
                }
                finally
                {
                    Log.Information("WS Closed");
                }
            }

            Log.Information("WS Closed");
        }

        public async Task Close(CancellationToken clt)
        {
            if (_clientWebSocket.State == WebSocketState.Open)
            {
                await _clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, clt);
            }
        }

        private async Task OnBlockReceived(byte[] binary, CancellationToken clt)
        {
            if (_headBlockNum == 0)
            {
                var getStatusResult = _abiSerializationProvider.Deserialize<GetStatusResultV0>(binary);
                _headBlockNum = getStatusResult.Head.BlockNum;
                return;
            }
            await _channel.WriteAsync(binary, clt);
        }

        private void OnTextMessageReceived(string message)
        {
            if (_abi == null)
            {
                _abi = JsonConvert.DeserializeObject<Abi>(message);
                Log.Information("abiversion is:" + _abi.version);
            }
        }

        public async Task SendStatusRequest(GetStatusRequestV0 statusRequest, CancellationToken clt)
        {
            while (_clientWebSocket.State != WebSocketState.Open || _abi == null)
            {
                await Task.Delay(50, clt);
            }

            var serializedData = _abiSerializationProvider.Serialize("request",
                new KeyValuePair<string, object>("get_status_request_v0", statusRequest), _abi);

            await SendMessage(WebSocketMessageType.Binary,
                new ArraySegment<byte>(serializedData, 0, serializedData.Length), clt);
        }

        public async Task SendBlocksAckRequest(uint numMessages, CancellationToken clt)
        {
            await SendBlocksRequest(new GetBlocksAckRequestV0(numMessages), clt);
        }

        public async Task SendBlocksRequest(object request, CancellationToken clt)
        {
            while (_clientWebSocket.State != WebSocketState.Open || _abi == null)
            {
                await Task.Delay(50, clt);
            }

            byte[] serializedData = new byte[] { };
            if(request is GetBlocksRequestV0 getBlocksRequestV0)
                serializedData = _abiSerializationProvider.Serialize("request",
                new KeyValuePair<string, object>("get_blocks_request_v0", getBlocksRequestV0), _abi);
            else if (request is GetBlocksAckRequestV0 getBlocksAckRequestV0)
                serializedData = _abiSerializationProvider.Serialize("request",
                    new KeyValuePair<string, object>("get_blocks_ack_request_v0", getBlocksAckRequestV0), _abi);

            await SendMessage(WebSocketMessageType.Binary,
                new ArraySegment<byte>(serializedData, 0, serializedData.Length), clt);
        }

        private async Task SendMessage(WebSocketMessageType messageType, ArraySegment<byte> buffer, CancellationToken clt)
        {
            if (buffer.Count == 0)
                return;

            try
            {
                await _clientWebSocket.SendAsync(buffer, messageType, true, clt);
//                t.Wait(_cancellationToken);
            }
            catch (Exception ex)
            {
                Log.Error(ex,"");
            }
        }
    }
}
