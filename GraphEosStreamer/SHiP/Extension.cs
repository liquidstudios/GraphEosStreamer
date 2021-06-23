using System;
using Newtonsoft.Json;

namespace GraphEosStreamer.SHiP
{
    [Serializable()]
    public class Extension
    {

        // abi-field-name: type ,abi-field-type: uint16
        [JsonProperty("type")]
        public ushort Type;

        // abi-field-name: data ,abi-field-type: bytes
        [JsonProperty("data")]
        public Bytes Data;

        public Extension(ushort type, Bytes data)
        {
            this.Type = type;
            this.Data = data;
        }

        public Extension()
        {
        }
    }
}