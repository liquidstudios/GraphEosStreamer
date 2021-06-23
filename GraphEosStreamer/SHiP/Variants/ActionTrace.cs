using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;


namespace GraphEosStreamer.SHiP.Variants
{
    public abstract class ActionTrace
    {
        public abstract string ToJson();
    }

    [Serializable()]
    public class ActionTraceV0 : ActionTrace
    {

        // abi-field-name: action_ordinal ,abi-field-type: varuint32
        [JsonProperty("action_ordinal")]
        public VarUint32 ActionOrdinal;

        // abi-field-name: creator_action_ordinal ,abi-field-type: varuint32
        [JsonProperty("creator_action_ordinal")]
        public VarUint32 CreatorActionOrdinal;

        // abi-field-name: receipt ,abi-field-type: action_receipt?
        [JsonProperty("receipt")]
        public ActionReceipt? Receipt;

        // abi-field-name: receiver ,abi-field-type: name
        [JsonProperty("receiver")]
        public Name Receiver;

        // abi-field-name: act ,abi-field-type: action
        [JsonProperty("act")]
        public Action Act;

        // abi-field-name: context_free ,abi-field-type: bool
        [JsonProperty("context_free")]
        public bool ContextFree;

        // abi-field-name: elapsed ,abi-field-type: int64
        [JsonProperty("elapsed")]
        public long Elapsed;

        // abi-field-name: console ,abi-field-type: string
        [JsonProperty("console")]
        public string Console;

        // abi-field-name: account_ram_deltas ,abi-field-type: account_delta[]
        [JsonProperty("account_ram_deltas")]
        public AccountDelta[] AccountRamDeltas;

        // abi-field-name: except ,abi-field-type: string?
        [JsonProperty("except")]
        public string? Except;

        // abi-field-name: error_code ,abi-field-type: uint64?
        [JsonProperty("error_code")]
        public ulong? ErrorCode;

        public ActionTraceV0(VarUint32 actionOrdinal, VarUint32 creatorActionOrdinal, ActionReceipt? receipt, Name receiver, Action act, bool contextFree, long elapsed, string console, AccountDelta[] accountRamDeltas, string? except, ulong? errorCode)
        {
            this.ActionOrdinal = actionOrdinal;
            this.CreatorActionOrdinal = creatorActionOrdinal;
            this.Receipt = receipt;
            this.Receiver = receiver;
            this.Act = act;
            this.ContextFree = contextFree;
            this.Elapsed = elapsed;
            this.Console = console;
            this.AccountRamDeltas = accountRamDeltas;
            this.Except = except;
            this.ErrorCode = errorCode;
        }

        public ActionTraceV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}