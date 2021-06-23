using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.Variants;


namespace GraphEosStreamer.SHiP.Tables
{
    public abstract class ProtocolState : Table
    {

    }

    [Serializable()]
    public class ProtocolStateV0 : ProtocolState
    {

        // abi-field-name: activated_protocol_features ,abi-field-type: activated_protocol_feature[]
        [JsonProperty("activated_protocol_features")]
        public ActivatedProtocolFeatureV0[] ActivatedProtocolFeatures;

        public ProtocolStateV0(ActivatedProtocolFeatureV0[] activatedProtocolFeatures)
        {
            this.ActivatedProtocolFeatures = activatedProtocolFeatures;
        }

        public ProtocolStateV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}