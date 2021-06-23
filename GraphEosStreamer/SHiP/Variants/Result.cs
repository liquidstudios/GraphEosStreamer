using System;
using Newtonsoft.Json;


namespace GraphEosStreamer.SHiP.Variants
{
    public abstract class Result
    {
        public abstract string ToJson();
    }

    [Serializable()]
    public class GetStatusResultV0 : Result
    {

        // abi-field-name: head ,abi-field-type: block_position
        [JsonProperty("head")]
        public BlockPosition Head;

        // abi-field-name: last_irreversible ,abi-field-type: block_position
        [JsonProperty("last_irreversible")]
        public BlockPosition LastIrreversible;

        // abi-field-name: trace_begin_block ,abi-field-type: uint32
        [JsonProperty("trace_begin_block")]
        public uint TraceBeginBlock;

        // abi-field-name: trace_end_block ,abi-field-type: uint32
        [JsonProperty("trace_end_block")]
        public uint TraceEndBlock;

        // abi-field-name: chain_state_begin_block ,abi-field-type: uint32
        [JsonProperty("chain_state_begin_block")]
        public uint ChainStateBeginBlock;

        // abi-field-name: chain_state_end_block ,abi-field-type: uint32
        [JsonProperty("chain_state_end_block")]
        public uint ChainStateEndBlock;

        public GetStatusResultV0(BlockPosition head, BlockPosition lastIrreversible, uint traceBeginBlock, uint traceEndBlock, uint chainStateBeginBlock, uint chainStateEndBlock)
        {
            this.Head = head;
            this.LastIrreversible = lastIrreversible;
            this.TraceBeginBlock = traceBeginBlock;
            this.TraceEndBlock = traceEndBlock;
            this.ChainStateBeginBlock = chainStateBeginBlock;
            this.ChainStateEndBlock = chainStateEndBlock;
        }

        public GetStatusResultV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [Serializable()]
    public class GetBlocksResultV0 : Result
    {

        // abi-field-name: head ,abi-field-type: block_position
        [JsonProperty("head")]
        public BlockPosition Head;

        // abi-field-name: last_irreversible ,abi-field-type: block_position
        [JsonProperty("last_irreversible")]
        public BlockPosition LastIrreversible;

        // abi-field-name: this_block ,abi-field-type: block_position?
        [JsonProperty("this_block")]
        public BlockPosition? ThisBlock;

        // abi-field-name: prev_block ,abi-field-type: block_position?
        [JsonProperty("prev_block")]
        public BlockPosition? PrevBlock;

        // abi-field-name: block ,abi-field-type: bytes?
        [JsonProperty("block")]
        public BlockBytes? BlockBytes;

        // abi-field-name: traces ,abi-field-type: bytes?
        [JsonProperty("traces")]
        public TracesBytes? TracesBytes;
        //        public TransactionTraceVariant[]? Traces;

        // abi-field-name: deltas ,abi-field-type: bytes?
        [JsonProperty("deltas")]
        public DeltasBytes? DeltasBytes;

        public GetBlocksResultV0(BlockPosition head, BlockPosition lastIrreversible, BlockPosition? thisBlock, BlockPosition? prevBlock, BlockBytes blockBytes, TracesBytes tracesBytes, DeltasBytes deltasBytes)
        {
            this.Head = head;
            this.LastIrreversible = lastIrreversible;
            this.ThisBlock = thisBlock;
            this.PrevBlock = prevBlock;
            this.BlockBytes = blockBytes;
            this.TracesBytes = tracesBytes;
            this.DeltasBytes = deltasBytes;
        }

        public GetBlocksResultV0()
        {
        }

        public override string ToJson()
        {
                        return JsonConvert.SerializeObject(this);
        }
    }
}