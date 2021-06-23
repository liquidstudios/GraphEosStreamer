using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;


namespace GraphEosStreamer.SHiP.Tables
{
    public abstract class ContractIndexLongDouble : Table
    {

    }

    [Serializable()]
    public class ContractIndexLongDoubleV0 : ContractIndexLongDouble
    {

        // abi-field-name: code ,abi-field-type: name
        [JsonProperty("code")]
        public Name Code;

        // abi-field-name: scope ,abi-field-type: name
        [JsonProperty("scope")]
        public Name Scope;

        // abi-field-name: table ,abi-field-type: name
        [JsonProperty("table")]
        public Name Table;

        // abi-field-name: primary_key ,abi-field-type: uint64
        [JsonProperty("primary_key")]
        public ulong PrimaryKey;

        // abi-field-name: payer ,abi-field-type: name
        [JsonProperty("payer")]
        public Name Payer;

        // abi-field-name: secondary_key ,abi-field-type: float128
        [JsonProperty("secondary_key")]
        public Float128 SecondaryKey;

        public ContractIndexLongDoubleV0(Name code, Name scope, Name table, ulong primaryKey, Name payer, byte[] secondaryKey)
        {
            this.Code = code;
            this.Scope = scope;
            this.Table = table;
            this.PrimaryKey = primaryKey;
            this.Payer = payer;
            this.SecondaryKey = secondaryKey;
        }

        public ContractIndexLongDoubleV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}