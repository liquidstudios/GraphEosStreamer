using System;
using Newtonsoft.Json;
using GraphEosStreamer.Other;
using GraphEosStreamer.SHiP.EosTypes;

namespace GraphEosStreamer.SHiP
{
    [Serializable()]
    public class TransactionReceiptHeader
    {

        // abi-field-name: status ,abi-field-type: uint8
        [SortOrder(1)]
        [JsonProperty("status")]
        public byte Status;

        // abi-field-name: cpu_usage_us ,abi-field-type: uint32
        [SortOrder(2)]
        [JsonProperty("cpu_usage_us")]
        public uint CpuUsageUs;

        // abi-field-name: net_usage_words ,abi-field-type: varuint32
        [SortOrder(3)]
        [JsonProperty("net_usage_words")]
        public VarUint32 NetUsageWords;

        public TransactionReceiptHeader(byte status, uint cpuUsageUs, VarUint32 netUsageWords)
        {
            this.Status = status;
            this.CpuUsageUs = cpuUsageUs;
            this.NetUsageWords = netUsageWords;
        }

        public TransactionReceiptHeader()
        {
        }
    }
}