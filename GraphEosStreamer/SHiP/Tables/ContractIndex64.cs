using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;


namespace GraphEosStreamer.SHiP.Tables
{
    public abstract class ContractIndex64 : Table
    {

    }

    [Serializable()]
    public class ContractIndex64V0 : ContractIndex64
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

        // abi-field-name: secondary_key ,abi-field-type: uint64
        [JsonProperty("secondary_key")]
        public ulong SecondaryKey;

        public ContractIndex64V0(Name code, Name scope, Name table, ulong primaryKey, Name payer, ulong secondaryKey)
        {
            this.Code = code;
            this.Scope = scope;
            this.Table = table;
            this.PrimaryKey = primaryKey;
            this.Payer = payer;
            this.SecondaryKey = secondaryKey;
        }

        public ContractIndex64V0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}