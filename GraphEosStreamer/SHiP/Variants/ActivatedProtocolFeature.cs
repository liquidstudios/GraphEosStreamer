using System;
using Newtonsoft.Json;


namespace GraphEosStreamer.SHiP.Variants
{
    public abstract class ActivatedProtocolFeature
    {
        public abstract string ToJson();
    }

    [Serializable()]
    public class ActivatedProtocolFeatureV0 : ActivatedProtocolFeature
    {

        // abi-field-name: feature_digest ,abi-field-type: checksum256
        [JsonProperty("feature_digest")]
        public Checksum256 FeatureDigest;

        // abi-field-name: activation_block_num ,abi-field-type: uint32
        [JsonProperty("activation_block_num")]
        public uint ActivationBlockNum;

        public ActivatedProtocolFeatureV0(string featureDigest, uint activationBlockNum)
        {
            this.FeatureDigest = featureDigest;
            this.ActivationBlockNum = activationBlockNum;
        }

        public ActivatedProtocolFeatureV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}