using System;
using Newtonsoft.Json;


namespace GraphEosStreamer.SHiP.Variants
{
    public abstract class Request
    {
        public abstract string ToJson();
    }

    [Serializable()]
    public class GetStatusRequestV0 : Request
    {
        public GetStatusRequestV0()
        {
        }


        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [Serializable()]
    public class GetBlocksRequestV0 : Request
    {

        // abi-field-name: start_block_num ,abi-field-type: uint32
        [JsonProperty("start_block_num")]
        public uint StartBlockNum;

        // abi-field-name: end_block_num ,abi-field-type: uint32
        [JsonProperty("end_block_num")]
        public uint EndBlockNum;

        // abi-field-name: max_messages_in_flight ,abi-field-type: uint32
        [JsonProperty("max_messages_in_flight")]
        public uint MaxMessagesInFlight;

        // abi-field-name: have_positions ,abi-field-type: block_position[]
        [JsonProperty("have_positions")]
        public BlockPosition[] HavePositions;

        // abi-field-name: irreversible_only ,abi-field-type: bool
        [JsonProperty("irreversible_only")]
        public bool IrreversibleOnly;

        // abi-field-name: fetch_block ,abi-field-type: bool
        [JsonProperty("fetch_block")]
        public bool FetchBlock;

        // abi-field-name: fetch_traces ,abi-field-type: bool
        [JsonProperty("fetch_traces")]
        public bool FetchTraces;

        // abi-field-name: fetch_deltas ,abi-field-type: bool
        [JsonProperty("fetch_deltas")]
        public bool FetchDeltas;

        public GetBlocksRequestV0(uint startBlockNum, uint endBlockNum, uint maxMessagesInFlight, BlockPosition[] havePositions, bool irreversibleOnly, bool fetchBlock, bool fetchTraces, bool fetchDeltas)
        {
            this.StartBlockNum = startBlockNum;
            this.EndBlockNum = endBlockNum;
            this.MaxMessagesInFlight = maxMessagesInFlight;
            this.HavePositions = havePositions;
            this.IrreversibleOnly = irreversibleOnly;
            this.FetchBlock = fetchBlock;
            this.FetchTraces = fetchTraces;
            this.FetchDeltas = fetchDeltas;
        }

        public GetBlocksRequestV0()
        {
        }

        public override string ToJson()
        {
                        return JsonConvert.SerializeObject(this);
        }
    }

    [Serializable()]
    public class GetBlocksAckRequestV0 : Request
    {

        // abi-field-name: num_messages ,abi-field-type: uint32
        [JsonProperty("num_messages")]
        public uint NumMessages;

        public GetBlocksAckRequestV0(uint numMessages)
        {
            this.NumMessages = numMessages;
        }

        public GetBlocksAckRequestV0()
        {
        }

        public override string ToJson()
        {
                        return JsonConvert.SerializeObject(this);
        }
    }
}