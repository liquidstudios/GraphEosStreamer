using System;
using Newtonsoft.Json;


namespace GraphEosStreamer.SHiP.Variants
{
    public abstract class ElasticLimitParameters
    {
        public abstract string ToJson();
    }

    [Serializable()]
    public class ElasticLimitParametersV0 : ElasticLimitParameters
    {

        // abi-field-name: target ,abi-field-type: uint64
        [JsonProperty("target")]
        public ulong Target;

        // abi-field-name: max ,abi-field-type: uint64
        [JsonProperty("max")]
        public ulong Max;

        // abi-field-name: periods ,abi-field-type: uint32
        [JsonProperty("periods")]
        public uint Periods;

        // abi-field-name: max_multiplier ,abi-field-type: uint32
        [JsonProperty("max_multiplier")]
        public uint MaxMultiplier;

        // abi-field-name: contract_rate ,abi-field-type: resource_limits_ratio
        [JsonProperty("contract_rate")]
        public ResourceLimitsRatio ContractRate;

        // abi-field-name: expand_rate ,abi-field-type: resource_limits_ratio
        [JsonProperty("expand_rate")]
        public ResourceLimitsRatio ExpandRate;

        public ElasticLimitParametersV0(ulong target, ulong max, uint periods, uint maxMultiplier, ResourceLimitsRatioV0 contractRate, ResourceLimitsRatioV0 expandRate)
        {
            this.Target = target;
            this.Max = max;
            this.Periods = periods;
            this.MaxMultiplier = maxMultiplier;
            this.ContractRate = contractRate;
            this.ExpandRate = expandRate;
        }

        public ElasticLimitParametersV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}