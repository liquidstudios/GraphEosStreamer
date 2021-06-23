using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;

namespace GraphEosStreamer.SHiP
{
    [Serializable()]
    public class PermissionLevel
    {

        // abi-field-name: actor ,abi-field-type: name
        [JsonProperty("actor")]
        public Name Actor;

        // abi-field-name: permission ,abi-field-type: name
        [JsonProperty("permission")]
        public Name Permission;

        public PermissionLevel(Name actor, Name permission)
        {
            this.Actor = actor;
            this.Permission = permission;
        }

        public PermissionLevel()
        {
        }
    }
}