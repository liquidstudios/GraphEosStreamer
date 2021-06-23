using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;


namespace GraphEosStreamer.SHiP.Variants
{
    public abstract class ActionReceipt
    {
        public abstract string ToJson();
    }

    [Serializable()]
    public class ActionReceiptV0 : ActionReceipt
    {

        // abi-field-name: receiver ,abi-field-type: name
        [JsonProperty("receiver")]
        public Name Receiver;

        // abi-field-name: act_digest ,abi-field-type: checksum256
        [JsonProperty("act_digest")]
        public Checksum256 ActDigest;

        // abi-field-name: global_sequence ,abi-field-type: uint64
        [JsonProperty("global_sequence")]
        public ulong GlobalSequence;

        // abi-field-name: recv_sequence ,abi-field-type: uint64
        [JsonProperty("recv_sequence")]
        public ulong RecvSequence;

        // abi-field-name: auth_sequence ,abi-field-type: account_auth_sequence[]
        [JsonProperty("auth_sequence")]
        public AccountAuthSequence[] AuthSequence;

        // abi-field-name: code_sequence ,abi-field-type: varuint32
        [JsonProperty("code_sequence")]
        public VarUint32 CodeSequence;

        // abi-field-name: abi_sequence ,abi-field-type: varuint32
        [JsonProperty("abi_sequence")]
        public VarUint32 AbiSequence;

        public ActionReceiptV0(Name receiver, string actDigest, ulong globalSequence, ulong recvSequence, AccountAuthSequence[] authSequence, VarUint32 codeSequence, VarUint32 abiSequence)
        {
            this.Receiver = receiver;
            this.ActDigest = actDigest;
            this.GlobalSequence = globalSequence;
            this.RecvSequence = recvSequence;
            this.AuthSequence = authSequence;
            this.CodeSequence = codeSequence;
            this.AbiSequence = abiSequence;
        }

        public ActionReceiptV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}