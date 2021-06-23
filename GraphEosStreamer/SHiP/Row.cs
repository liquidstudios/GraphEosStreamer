using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.Tables;

namespace GraphEosStreamer.SHiP
{
    [Serializable()]
    public class Row
    {
        // abi-field-name: present ,abi-field-type: bool
        [JsonProperty("present")]
        public bool Present;

        // abi-field-name: data ,abi-field-type: bytes
        //[JsonProperty("data")]
        [JsonIgnore]
        public Bytes Data;

        [JsonProperty("data")]
        public Table? Table;

        public Row(bool present, byte[] data)
        {
            this.Present = present;
            this.Data = data;
        }

        public Row()
        {
        }
    }
}