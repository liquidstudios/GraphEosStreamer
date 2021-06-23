using System;
using Newtonsoft.Json;

namespace GraphEosStreamer.SHiP
{
    [Serializable()]
    public class Authority
    {

        // abi-field-name: threshold ,abi-field-type: uint32
        [JsonProperty("threshold")]
        public uint Threshold;

        // abi-field-name: keys ,abi-field-type: key_weight[]
        [JsonProperty("keys")]
        public KeyWeight[] Keys;

        // abi-field-name: accounts ,abi-field-type: permission_level_weight[]
        [JsonProperty("accounts")]
        public PermissionLevelWeight[] Accounts;

        // abi-field-name: waits ,abi-field-type: wait_weight[]
        [JsonProperty("waits")]
        public WaitWeight[] Waits;

        public Authority(uint threshold, KeyWeight[] keys, PermissionLevelWeight[] accounts, WaitWeight[] waits)
        {
            this.Threshold = threshold;
            this.Keys = keys;
            this.Accounts = accounts;
            this.Waits = waits;
        }

        public Authority()
        {
        }
    }
}