using System;
using Newtonsoft.Json;


namespace GraphEosStreamer.SHiP.Variants
{
    public abstract class UsageAccumulator
    {
        public abstract string ToJson();
    }

    [Serializable()]
    public class UsageAccumulatorV0 : UsageAccumulator
    {

        // abi-field-name: last_ordinal ,abi-field-type: uint32
        [JsonProperty("last_ordinal")]
        public uint LastOrdinal;

        // abi-field-name: value_ex ,abi-field-type: uint64
        [JsonProperty("value_ex")]
        public ulong ValueEx;

        // abi-field-name: consumed ,abi-field-type: uint64
        [JsonProperty("consumed")]
        public ulong Consumed;

        public UsageAccumulatorV0(uint lastOrdinal, ulong valueEx, ulong consumed)
        {
            this.LastOrdinal = lastOrdinal;
            this.ValueEx = valueEx;
            this.Consumed = consumed;
        }

        public UsageAccumulatorV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}