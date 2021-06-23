using System;
using Newtonsoft.Json;


namespace GraphEosStreamer.SHiP.Variants
{
    public abstract class ResourceLimitsRatio
    {
        public abstract string ToJson();
    }

    [Serializable()]
    public class ResourceLimitsRatioV0 : ResourceLimitsRatio
    {

        // abi-field-name: numerator ,abi-field-type: uint64
        [JsonProperty("numerator")]
        public ulong Numerator;

        // abi-field-name: denominator ,abi-field-type: uint64
        [JsonProperty("denominator")]
        public ulong Denominator;

        public ResourceLimitsRatioV0(ulong numerator, ulong denominator)
        {
            this.Numerator = numerator;
            this.Denominator = denominator;
        }

        public ResourceLimitsRatioV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}