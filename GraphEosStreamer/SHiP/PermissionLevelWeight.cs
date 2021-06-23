using System;
using Newtonsoft.Json;

namespace GraphEosStreamer.SHiP
{
    [Serializable()]
    public class PermissionLevelWeight
    {

        // abi-field-name: permission ,abi-field-type: permission_level
        [JsonProperty("permission")]
        public PermissionLevel Permission;

        // abi-field-name: weight ,abi-field-type: uint16
        [JsonProperty("weight")]
        public ushort Weight;

        public PermissionLevelWeight(PermissionLevel permission, ushort weight)
        {
            this.Permission = permission;
            this.Weight = weight;
        }

        public PermissionLevelWeight()
        {
        }
    }
}