using Newtonsoft.Json;

namespace GraphEosStreamer.SHiP
{
    public class ProducerSchedule
    {

        // abi-field-name: version ,abi-field-type: uint32
        [JsonProperty("version")]
        public uint Version;

        // abi-field-name: producers ,abi-field-type: producer_key[]
        [JsonProperty("producers")]
        public ProducerKey[] Producers;

        public ProducerSchedule(uint version, ProducerKey[] producers)
        {
            this.Version = version;
            this.Producers = producers;
        }

        public ProducerSchedule()
        {
        }
    }
}