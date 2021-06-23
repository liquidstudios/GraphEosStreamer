using System;
using Newtonsoft.Json;

namespace GraphEosStreamer.SHiP
{
    [Serializable()]
    public class ProducerAuthoritySchedule
    {

        // abi-field-name: version ,abi-field-type: uint32
        [JsonProperty("version")]
        public uint Version;

        // abi-field-name: producers ,abi-field-type: producer_authority[]
        [JsonProperty("producers")]
        public ProducerAuthority[] Producers;

        public ProducerAuthoritySchedule(uint version, ProducerAuthority[] producers)
        {
            this.Version = version;
            this.Producers = producers;
        }

        public ProducerAuthoritySchedule()
        {
        }
    }
}