using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using GraphEosStreamer.SHiP.Variants;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

namespace GraphEosStreamer.BlockReader
{
    public class BlockDeserializerService : BackgroundService
    {
        private readonly ChannelReader<byte[]> _binaryDataChannel;
        private readonly ChannelWriter<GetBlocksResultV0> _blocksResultChannel;
        private readonly BlockReaderService _blockReaderService;
        private readonly Options _options;

        private readonly int _blockDeserializerTasks;
        private uint _maxMessagesInFlight;
        private volatile int _blockReceived;

        public BlockDeserializerService(BlockReaderService blockReaderService, ChannelReader<byte[]> binaryDataChannel,
            ChannelWriter<GetBlocksResultV0> blocksResultChannel, IOptionsMonitor<Options> optionsMonitor)
        {
            _options = optionsMonitor.CurrentValue;

            _blockReaderService = blockReaderService;
            _binaryDataChannel = binaryDataChannel;
            _blocksResultChannel = blocksResultChannel;

            _blockDeserializerTasks = _options.BlockDeserializerTasks;

            optionsMonitor.OnChange(OnOptionsChanged);
        }

        private void OnOptionsChanged(Options options)
        {
            _maxMessagesInFlight = options.MaxMessagesInFlight;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();
            for (var i = 0; i < _blockDeserializerTasks; i++)
            {
                tasks.Add(DeserializeBlocks(i, cancellationToken));
            }

            tasks.Add(WaitToSendAckAsync(cancellationToken));
            await Task.WhenAll(tasks);
        }

        private async Task DeserializeBlocks(int taskNum, CancellationToken clt)
        {
            await foreach (var binary in _binaryDataChannel.ReadAllAsync(clt))
            {
                Interlocked.Increment(ref _blockReceived);
                try
                {
                    var result = (await Deserializer.Deserializer.DeserializeAsync<Result>(binary, clt));
                    switch (result)
                    {
                        case GetBlocksResultV0 getBlocksResultV0:
                        {
                            var childDeserializers = new List<Task>();

                            if (getBlocksResultV0.BlockBytes != null)
                                childDeserializers.Add(getBlocksResultV0.BlockBytes.DeserializeAsync(clt));
                            if (getBlocksResultV0.TracesBytes != null)
                                childDeserializers.Add(getBlocksResultV0.TracesBytes.DeserializeAsync(clt));
                            if (getBlocksResultV0.DeltasBytes != null)
                                childDeserializers.Add(getBlocksResultV0.DeltasBytes.DeserializeAsync(clt));
            
                            if (childDeserializers.Count > 0)
                                await Task.WhenAll(childDeserializers);

                            await _blocksResultChannel.WriteAsync(getBlocksResultV0, clt);
                            if(getBlocksResultV0.ThisBlock?.BlockNum % 50000 == 0)
                                Log.Information("task" + taskNum + " deserialized Block " + getBlocksResultV0.ThisBlock?.BlockNum + " at " + DateTime.Now.ToLongTimeString());
                            break;
                        }
                        case GetStatusResultV0 getStatusResultV0:
                            Log.Information($"received Status, head is {getStatusResultV0.Head}");
                            break;
                        default:
                            Log.Information("resulttype unknown or null");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e,"");
                }
            }
        }

        public async Task WaitToSendAckAsync(CancellationToken clt)
        {
            while (_binaryDataChannel.Count == 0)
            {
                await Task.Delay(2000, clt);
            }

            while (true)
            {
                if (_blockReceived > _maxMessagesInFlight * 0.4f) // request new Blocks at 40% of max messages in flight
                {
                    Log.Information($"Sending ack of {_blockReceived} blocks");
                    
                    var blocksReceived = _blockReceived;
                    await _blockReaderService.SendBlocksAckRequest((uint) blocksReceived, clt);
                    Interlocked.Add(ref _blockReceived, -blocksReceived);
                }
                await Task.Delay(2000, clt);
            }
        }
    }
}
