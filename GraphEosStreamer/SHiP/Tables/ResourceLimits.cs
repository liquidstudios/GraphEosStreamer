using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;


namespace GraphEosStreamer.SHiP.Tables
{
    public abstract class ResourceLimits : Table
    {

    }

    [Serializable()]
    public class ResourceLimitsV0 : ResourceLimits
    {

        // abi-field-name: owner ,abi-field-type: name
        [JsonProperty("owner")]
        public Name Owner;

        // abi-field-name: net_weight ,abi-field-type: int64
        [JsonProperty("net_weight")]
        public long NetWeight;

        // abi-field-name: cpu_weight ,abi-field-type: int64
        [JsonProperty("cpu_weight")]
        public long CpuWeight;

        // abi-field-name: ram_bytes ,abi-field-type: int64
        [JsonProperty("ram_bytes")]
        public long RamBytes;

        public ResourceLimitsV0(Name owner, long netWeight, long cpuWeight, long ramBytes)
        {
            this.Owner = owner;
            this.NetWeight = netWeight;
            this.CpuWeight = cpuWeight;
            this.RamBytes = ramBytes;
        }

        public ResourceLimitsV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}