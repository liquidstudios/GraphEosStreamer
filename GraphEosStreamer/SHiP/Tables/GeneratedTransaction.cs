using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;
using GraphEosStreamer.SHiP.Variants;


namespace GraphEosStreamer.SHiP.Tables
{
    public abstract class GeneratedTransaction : Table
    {

    }

    [Serializable()]
    public class GeneratedTransactionV0 : GeneratedTransaction
    {

        // abi-field-name: sender ,abi-field-type: name
        [JsonProperty("sender")]
        public Name Sender;

        // abi-field-name: sender_id ,abi-field-type: uint128
        [JsonProperty("sender_id")]
        public Uint128 SenderId;

        // abi-field-name: payer ,abi-field-type: name
        [JsonProperty("payer")]
        public Name Payer;

        // abi-field-name: trx_id ,abi-field-type: checksum256
        [JsonProperty("trx_id")]
        public Checksum256 TrxId;

        // abi-field-name: packed_trx ,abi-field-type: bytes
        [JsonProperty("packed_trx")]
        public PackedTransactionBytes PackedTrx;

        public GeneratedTransactionV0(Name sender, byte[] senderId, Name payer, string trxId, byte[] packedTrx)
        {
            this.Sender = sender;
            this.SenderId = senderId;
            this.Payer = payer;
            this.TrxId = trxId;
            this.PackedTrx = (PackedTransactionBytes)packedTrx;
        }

        public GeneratedTransactionV0()
        {
        }

        public override string ToJson()
        { 
            return JsonConvert.SerializeObject(this);
        }
    }
}