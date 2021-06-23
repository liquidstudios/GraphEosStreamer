using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.Variants;


namespace GraphEosStreamer.SHiP.Tables
{
    public abstract class ResourceLimitsConfig : Table
    {

    }

    [Serializable()]
    public class ResourceLimitsConfigV0 : ResourceLimitsConfig
    {

        // abi-field-name: cpu_limit_parameters ,abi-field-type: elastic_limit_parameters
        [JsonProperty("cpu_limit_parameters")]
        public ElasticLimitParameters CpuLimitParameters;

        // abi-field-name: net_limit_parameters ,abi-field-type: elastic_limit_parameters
        [JsonProperty("net_limit_parameters")]
        public ElasticLimitParameters NetLimitParameters;

        // abi-field-name: account_cpu_usage_average_window ,abi-field-type: uint32
        [JsonProperty("account_cpu_usage_average_window")]
        public uint AccountCpuUsageAverageWindow;

        // abi-field-name: account_net_usage_average_window ,abi-field-type: uint32
        [JsonProperty("account_net_usage_average_window")]
        public uint AccountNetUsageAverageWindow;

        public ResourceLimitsConfigV0(ElasticLimitParameters cpuLimitParameters, ElasticLimitParameters netLimitParameters, uint accountCpuUsageAverageWindow, uint accountNetUsageAverageWindow)
        {
            this.CpuLimitParameters = cpuLimitParameters;
            this.NetLimitParameters = netLimitParameters;
            this.AccountCpuUsageAverageWindow = accountCpuUsageAverageWindow;
            this.AccountNetUsageAverageWindow = accountNetUsageAverageWindow;
        }

        public ResourceLimitsConfigV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}