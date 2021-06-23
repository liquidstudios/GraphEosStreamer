using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;


namespace GraphEosStreamer.SHiP.Tables
{
    public abstract class AccountMetadata : Table
    {

    }

    [Serializable()]
    public class AccountMetadataV0 : AccountMetadata
    {
        // abi-field-name: name ,abi-field-type: name
        [JsonProperty("name")]
        public Name Name;

        // abi-field-name: privileged ,abi-field-type: bool
        [JsonProperty("privileged")]
        public bool Privileged;

        // abi-field-name: last_code_update ,abi-field-type: time_point
        [JsonProperty("last_code_update")]
        public ulong LastCodeUpdate;

        // abi-field-name: code ,abi-field-type: code_id?
        [JsonProperty("code")]
        public CodeId? Code;

        public AccountMetadataV0(Name name, bool privileged, uint lastCodeUpdate, CodeId? code)
        {
            this.Name = name;
            this.Privileged = privileged;
            this.LastCodeUpdate = lastCodeUpdate;
            this.Code = code;
        }

        public AccountMetadataV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}