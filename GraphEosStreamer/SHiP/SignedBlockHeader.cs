using System;
using Newtonsoft.Json;
using GraphEosStreamer.Other;
using GraphEosStreamer.SHiP.EosTypes;

namespace GraphEosStreamer.SHiP
{
    [Serializable()]
    public class SignedBlockHeader : BlockHeader
    {

        // abi-field-name: producer_signature ,abi-field-type: signature
        [SortOrder(10)]
        [JsonProperty("producer_signature")]
        public Signature ProducerSignature;

        public SignedBlockHeader(string producerSignature)
        {
            this.ProducerSignature = producerSignature;
        }

        public SignedBlockHeader()
        {
        }
    }
}