using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;


namespace GraphEosStreamer.SHiP.Tables
{
    public abstract class ContractTable : Table
    {

    }

    [Serializable()]
    public class ContractTableV0 : ContractTable
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

        // abi-field-name: payer ,abi-field-type: name
        [JsonProperty("payer")]
        public Name Payer;

        public ContractTableV0(Name code, Name scope, Name table, Name payer)
        {
            this.Code = code;
            this.Scope = scope;
            this.Table = table;
            this.Payer = payer;
        }

        public ContractTableV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}