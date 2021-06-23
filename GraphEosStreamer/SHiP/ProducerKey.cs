using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;

namespace GraphEosStreamer.SHiP
{
    [Serializable()]
    public class ProducerKey
    {

        // abi-field-name: producer_name ,abi-field-type: name
        [JsonProperty("producer_name")]
        public Name ProducerName;

        // abi-field-name: block_signing_key ,abi-field-type: public_key
        [JsonProperty("block_signing_key")]
        public PublicKey BlockSigningKey;

        public ProducerKey(Name producerName, string blockSigningKey)
        {
            this.ProducerName = producerName;
            this.BlockSigningKey = blockSigningKey;
        }

        public ProducerKey()
        {
        }
    }
}