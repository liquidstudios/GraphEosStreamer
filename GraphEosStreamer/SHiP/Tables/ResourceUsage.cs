using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;
using GraphEosStreamer.SHiP.Variants;


namespace GraphEosStreamer.SHiP.Tables
{
    public abstract class ResourceUsage : Table
    {
    }

    [Serializable()]
    public class ResourceUsageV0 : ResourceUsage
    {

        // abi-field-name: owner ,abi-field-type: name
        [JsonProperty("owner")]
        public Name Owner;

        // abi-field-name: net_usage ,abi-field-type: usage_accumulator
        [JsonProperty("net_usage")]
        public UsageAccumulator NetUsage;

        // abi-field-name: cpu_usage ,abi-field-type: usage_accumulator
        [JsonProperty("cpu_usage")]
        public UsageAccumulator CpuUsage;

        // abi-field-name: ram_usage ,abi-field-type: uint64
        [JsonProperty("ram_usage")]
        public ulong RamUsage;

        public ResourceUsageV0(Name owner, UsageAccumulator netUsage, UsageAccumulator cpuUsage, ulong ramUsage)
        {
            this.Owner = owner;
            this.NetUsage = netUsage;
            this.CpuUsage = cpuUsage;
            this.RamUsage = ramUsage;
        }

        public ResourceUsageV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}