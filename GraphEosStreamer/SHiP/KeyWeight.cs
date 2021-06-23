using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;

namespace GraphEosStreamer.SHiP
{
    [Serializable()]
    public class KeyWeight
    {

        // abi-field-name: key ,abi-field-type: public_key
        [JsonProperty("key")]
        public PublicKey Key;

        // abi-field-name: weight ,abi-field-type: uint16
        [JsonProperty("weight")]
        public ushort Weight;

        public KeyWeight(string key, ushort weight)
        {
            this.Key = key;
            this.Weight = weight;
        }

        public KeyWeight()
        {
        }
    }
}