using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;

namespace GraphEosStreamer.SHiP
{
    [Serializable()]
    public class Action
    {
        // abi-field-name: account ,abi-field-type: name
        [JsonProperty("account")]
        public Name Account;

        // abi-field-name: name ,abi-field-type: name
        [JsonProperty("name")]
        public Name Name;

        // abi-field-name: authorization ,abi-field-type: permission_level[]
        [JsonProperty("authorization")]
        public PermissionLevel[] Authorization;

        // abi-field-name: data ,abi-field-type: bytes
        [JsonProperty("data")]
        public ActionBytes Data;

        public Action(Name account, Name name, PermissionLevel[] authorization, byte[] data)
        {
            this.Account = account;
            this.Name = name;
            this.Authorization = authorization;
            this.Data = data;
        }

        public Action()
        {
        }
    }
}