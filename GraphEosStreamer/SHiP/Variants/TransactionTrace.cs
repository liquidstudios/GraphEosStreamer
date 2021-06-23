using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;


namespace GraphEosStreamer.SHiP.Variants
{
    public abstract class TransactionTrace
    {
        public abstract string ToJson();
    }

    [Serializable()]
    public class TransactionTraceV0 : TransactionTrace
    {

        // abi-field-name: id ,abi-field-type: checksum256
        [JsonProperty("id")]
        public Checksum256 Id;

        // abi-field-name: status ,abi-field-type: uint8
        [JsonProperty("status")]
        public byte Status;

        // abi-field-name: cpu_usage_us ,abi-field-type: uint32
        [JsonProperty("cpu_usage_us")]
        public uint CpuUsageUs;

        // abi-field-name: net_usage_words ,abi-field-type: varuint32
        [JsonProperty("net_usage_words")]
        public VarUint32 NetUsageWords;

        // abi-field-name: elapsed ,abi-field-type: int64
        [JsonProperty("elapsed")]
        public long Elapsed;

        // abi-field-name: net_usage ,abi-field-type: uint64
        [JsonProperty("net_usage")]
        public ulong NetUsage;

        // abi-field-name: scheduled ,abi-field-type: bool
        [JsonProperty("scheduled")]
        public bool Scheduled;

        // abi-field-name: action_traces ,abi-field-type: action_trace[]
        [JsonProperty("action_traces")]
        public ActionTrace[] ActionTraces;

        // abi-field-name: account_ram_delta ,abi-field-type: account_delta?
        [JsonProperty("account_ram_delta")]
        public AccountDelta? AccountRamDelta;

        // abi-field-name: except ,abi-field-type: string?
        [JsonProperty("except")]
        public string? Except;

        // abi-field-name: error_code ,abi-field-type: uint64?
        [JsonProperty("error_code")]
        public ulong? ErrorCode;

        // abi-field-name: failed_dtrx_trace ,abi-field-type: transaction_trace?
        [JsonProperty("failed_dtrx_trace")]
        public TransactionTrace? FailedDtrxTrace;

        // abi-field-name: ,abi-field-type: partial_transaction?
        [JsonProperty("partial")]
        public PartialTransaction? Partial;

        public TransactionTraceV0(string id, byte status, uint cpuUsageUs, VarUint32 netUsageWords, long elapsed, ulong netUsage, bool scheduled, ActionTrace[] actionTraces, AccountDelta? accountRamDelta, string? except, ulong? errorCode, TransactionTrace? failedDtrxTrace, PartialTransaction? @partial)
        {
            this.Id = id;
            this.Status = status;
            this.CpuUsageUs = cpuUsageUs;
            this.NetUsageWords = netUsageWords;
            this.Elapsed = elapsed;
            this.NetUsage = netUsage;
            this.Scheduled = scheduled;
            this.ActionTraces = actionTraces;
            this.AccountRamDelta = accountRamDelta;
            this.Except = except;
            this.ErrorCode = errorCode;
            this.FailedDtrxTrace = failedDtrxTrace;
            this.Partial = @partial;
        }

        public TransactionTraceV0()
        {
        }

        public override string ToJson()
        { 
            return JsonConvert.SerializeObject(this);
        }
    }
}