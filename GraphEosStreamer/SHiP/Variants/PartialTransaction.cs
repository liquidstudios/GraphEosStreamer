using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;


namespace GraphEosStreamer.SHiP.Variants
{
    public abstract class PartialTransaction
    {
        public abstract string ToJson();
    }

    [Serializable()]
    public class PartialTransactionV0 : PartialTransaction
    {

        // abi-field-name: expiration ,abi-field-type: time_point_sec
        [JsonProperty("expiration")]
        public uint Expiration;

        // abi-field-name: ref_block_num ,abi-field-type: uint16
        [JsonProperty("ref_block_num")]
        public ushort RefBlockNum;

        // abi-field-name: ref_block_prefix ,abi-field-type: uint32
        [JsonProperty("ref_block_prefix")]
        public uint RefBlockPrefix;

        // abi-field-name: max_net_usage_words ,abi-field-type: varuint32
        [JsonProperty("max_net_usage_words")]
        public VarUint32 MaxNetUsageWords;

        // abi-field-name: max_cpu_usage_ms ,abi-field-type: uint8
        [JsonProperty("max_cpu_usage_ms")]
        public byte MaxCpuUsageMs;

        // abi-field-name: delay_sec ,abi-field-type: varuint32
        [JsonProperty("delay_sec")]
        public VarUint32 DelaySec;

        // abi-field-name: transaction_extensions ,abi-field-type: extension[]
        [JsonProperty("transaction_extensions")]
        public Extension[] TransactionExtensions;

        // abi-field-name: signatures ,abi-field-type: signature[]
        [JsonProperty("signatures")]
        public Signature[] Signatures;

        // abi-field-name: context_free_data ,abi-field-type: bytes[]
        [JsonProperty("context_free_data")]
        public Bytes ContextFreeData;

        public PartialTransactionV0(uint expiration, ushort refBlockNum, uint refBlockPrefix, VarUint32 maxNetUsageWords, byte maxCpuUsageMs, VarUint32 delaySec, Extension[] transactionExtensions, Signature[] signatures, Bytes contextFreeData)
        {
            this.Expiration = expiration;
            this.RefBlockNum = refBlockNum;
            this.RefBlockPrefix = refBlockPrefix;
            this.MaxNetUsageWords = maxNetUsageWords;
            this.MaxCpuUsageMs = maxCpuUsageMs;
            this.DelaySec = delaySec;
            this.TransactionExtensions = transactionExtensions;
            this.Signatures = signatures;
            this.ContextFreeData = contextFreeData;
        }

        public PartialTransactionV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}