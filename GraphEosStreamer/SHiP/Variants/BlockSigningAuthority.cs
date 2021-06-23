using System;
using Newtonsoft.Json;


namespace GraphEosStreamer.SHiP.Variants
{
    public abstract class BlockSigningAuthority
    {
        public abstract string ToJson();
    }

    [Serializable()]
    public class BlockSigningAuthorityV0 : BlockSigningAuthority
    {

        // abi-field-name: threshold ,abi-field-type: uint32
        [JsonProperty("threshold")]
        public uint Threshold;

        // abi-field-name: keys ,abi-field-type: key_weight[]
        [JsonProperty("keys")]
        public KeyWeight[] Keys;

        public BlockSigningAuthorityV0(uint threshold, KeyWeight[] keys)
        {
            this.Threshold = threshold;
            this.Keys = keys;
        }

        public BlockSigningAuthorityV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}