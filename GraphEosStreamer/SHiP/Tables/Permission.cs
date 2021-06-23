using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;


namespace GraphEosStreamer.SHiP.Tables
{
    public abstract class Permission : Table
    {

    }

    [Serializable()]
    public class PermissionV0 : Permission
    {

        // abi-field-name: owner ,abi-field-type: name
        [JsonProperty("owner")]
        public Name Owner;

        // abi-field-name: name ,abi-field-type: name
        [JsonProperty("name")]
        public Name Name;

        // abi-field-name: parent ,abi-field-type: name
        [JsonProperty("parent")]
        public Name Parent;

        // abi-field-name: last_updated ,abi-field-type: time_point
        [JsonProperty("last_updated")]
        public ulong LastUpdated;

        // abi-field-name: auth ,abi-field-type: authority
        [JsonProperty("auth")]
        public Authority Auth;

        public PermissionV0(Name owner, Name name, Name parent, uint lastUpdated, Authority auth)
        {
            this.Owner = owner;
            this.Name = name;
            this.Parent = parent;
            this.LastUpdated = lastUpdated;
            this.Auth = auth;
        }

        public PermissionV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}