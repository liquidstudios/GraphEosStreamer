using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;


namespace GraphEosStreamer.SHiP.Tables
{
    public abstract class PermissionLink : Table
    {

    }

    [Serializable()]
    public class PermissionLinkV0 : PermissionLink
    {

        // abi-field-name: account ,abi-field-type: name
        [JsonProperty("account")]
        public Name Account;

        // abi-field-name: code ,abi-field-type: name
        [JsonProperty("code")]
        public Name Code;

        // abi-field-name: message_type ,abi-field-type: name
        [JsonProperty("message_type")]
        public Name MessageType;

        // abi-field-name: required_permission ,abi-field-type: name
        [JsonProperty("required_permission")]
        public Name RequiredPermission;

        public PermissionLinkV0(Name account, Name code, Name messageType, Name requiredPermission)
        {
            this.Account = account;
            this.Code = code;
            this.MessageType = messageType;
            this.RequiredPermission = requiredPermission;
        }

        public PermissionLinkV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}