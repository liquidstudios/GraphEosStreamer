using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;
using GraphEosStreamer.SHiP.Variants;

namespace GraphEosStreamer.SHiP
{
    [Serializable()]
    public class ProducerAuthority
    {

        // abi-field-name: producer_name ,abi-field-type: name
        [JsonProperty("producer_name")]
        public Name ProducerName;

        // abi-field-name: authority ,abi-field-type: block_signing_authority
        [JsonProperty("authority")]
        public BlockSigningAuthority Authority;

        public ProducerAuthority(Name producerName, BlockSigningAuthority authority)
        {
            this.ProducerName = producerName;
            this.Authority = authority;
        }

        public ProducerAuthority()
        {
        }
    }
}