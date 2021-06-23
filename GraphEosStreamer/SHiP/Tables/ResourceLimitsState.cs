using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.Variants;


namespace GraphEosStreamer.SHiP.Tables
{
    public abstract class ResourceLimitsState : Table
    {

    }

    [Serializable()]
    public class ResourceLimitsStateV0 : ResourceLimitsState
    {

        // abi-field-name: average_block_net_usage ,abi-field-type: usage_accumulator
        [JsonProperty("average_block_net_usage")]
        public UsageAccumulator AverageBlockNetUsage;

        // abi-field-name: average_block_cpu_usage ,abi-field-type: usage_accumulator
        [JsonProperty("average_block_cpu_usage")]
        public UsageAccumulator AverageBlockCpuUsage;

        // abi-field-name: total_net_weight ,abi-field-type: uint64
        [JsonProperty("total_net_weight")]
        public ulong TotalNetWeight;

        // abi-field-name: total_cpu_weight ,abi-field-type: uint64
        [JsonProperty("total_cpu_weight")]
        public ulong TotalCpuWeight;

        // abi-field-name: total_ram_bytes ,abi-field-type: uint64
        [JsonProperty("total_ram_bytes")]
        public ulong TotalRamBytes;

        // abi-field-name: virtual_net_limit ,abi-field-type: uint64
        [JsonProperty("virtual_net_limit")]
        public ulong VirtualNetLimit;

        // abi-field-name: virtual_cpu_limit ,abi-field-type: uint64
        [JsonProperty("virtual_cpu_limit")]
        public ulong VirtualCpuLimit;

        public ResourceLimitsStateV0(UsageAccumulator averageBlockNetUsage, UsageAccumulator averageBlockCpuUsage, ulong totalNetWeight, ulong totalCpuWeight, ulong totalRamBytes, ulong virtualNetLimit, ulong virtualCpuLimit)
        {
            this.AverageBlockNetUsage = averageBlockNetUsage;
            this.AverageBlockCpuUsage = averageBlockCpuUsage;
            this.TotalNetWeight = totalNetWeight;
            this.TotalCpuWeight = totalCpuWeight;
            this.TotalRamBytes = totalRamBytes;
            this.VirtualNetLimit = virtualNetLimit;
            this.VirtualCpuLimit = virtualCpuLimit;
        }

        public ResourceLimitsStateV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}