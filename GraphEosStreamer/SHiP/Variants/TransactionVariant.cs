using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;

namespace GraphEosStreamer.SHiP.Variants
{

    public abstract class TransactionVariant
    {
        public abstract string ToJson();
    }

    public class Checksum256 : TransactionVariant
    {
        private string _value;

        public static implicit operator Checksum256(string value)
        {
            return new() { _value = value };
        }

        public static implicit operator string(Checksum256 value)
        {
            return value._value;
        }

        public override string ToJson()
        {
            return _value;
        }
    }

    [Serializable()]
    public class PackedTransactionVariant : TransactionVariant
    {

        // abi-field-name: signatures ,abi-field-type: signature[]
        [JsonProperty("signatures")]
        public Signature[] Signatures;

        // abi-field-name: compression ,abi-field-type: uint8
        [JsonProperty("compression")]
        public byte Compression;

        // abi-field-name: packed_context_free_data ,abi-field-type: bytes
        [JsonProperty("packed_context_free_data")]
        public Bytes PackedContextFreeData;

        // abi-field-name: packed_trx ,abi-field-type: bytes
        [JsonProperty("packed_trx")]
        public PackedTransactionBytes PackedTrx;

        public PackedTransactionVariant(Signature[] signatures, byte compression, Bytes packedContextFreeData, Bytes packedTrx)
        {
            this.Signatures = signatures;
            this.Compression = compression;
            this.PackedContextFreeData = packedContextFreeData;
            this.PackedTrx = (PackedTransactionBytes)packedTrx;
        }

        public PackedTransactionVariant()
        {
        }


        public override string ToJson()
        {
            string test = JsonConvert.SerializeObject(this, Formatting.Indented, new SignatureConverter(), new BytesConverter());
            return test;
        }
    }

    public enum Compression
    {
        None = 0,
        Zlib = 1
    }
}