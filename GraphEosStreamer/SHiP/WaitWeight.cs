using System;
using Newtonsoft.Json;

namespace GraphEosStreamer.SHiP
{
    [Serializable()]
    public class WaitWeight
    {

        // abi-field-name: wait_sec ,abi-field-type: uint32
        [JsonProperty("wait_sec")]
        public uint WaitSec;

        // abi-field-name: weight ,abi-field-type: uint16
        [JsonProperty("weight")]
        public ushort Weight;

        public WaitWeight(uint waitSec, ushort weight)
        {
            this.WaitSec = waitSec;
            this.Weight = weight;
        }

        public WaitWeight()
        {
        }
    }
}