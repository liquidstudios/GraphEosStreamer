using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;
using Newtonsoft.Json.Linq;


namespace GraphEosStreamer.SHiP.Tables
{
    public abstract class ContractRow : Table
    {

    }

    [Serializable()]
    public class ContractRowV0 : ContractRow
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

        // abi-field-name: value ,abi-field-type: bytes
        [JsonProperty("value")]
        public ContractRowBytes Value;

        public ContractRowV0(Name code, Name scope, Name table, ulong primaryKey, Name payer, byte[] value)
        {
            this.Code = code;
            this.Scope = scope;
            this.Table = table;
            this.PrimaryKey = primaryKey;
            this.Payer = payer;
            this.Value = value;
        }

        public ContractRowV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class ContractRowV0Converter : JsonConverter
    {
        private readonly Type[] _types;

        public ContractRowV0Converter(params Type[] types)
        {
            _types = types;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);

            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {
                JObject o = (JObject)t;
                IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();

                o.AddFirst(new JProperty("Keys", new JArray(propertyNames)));

                o.WriteTo(writer);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary");
        }

        public override bool CanRead => false;

        public override bool CanConvert(Type objectType)
        {
            return false;
        }
    }
}