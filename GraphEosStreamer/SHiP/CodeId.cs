using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.Variants;

namespace GraphEosStreamer.SHiP
{
    [Serializable()]
    public class CodeId
    {
        // abi-field-name: vm_type ,abi-field-type: uint8
        [JsonProperty("vm_type")]
        public byte VmType;

        // abi-field-name: vm_version ,abi-field-type: uint8
        [JsonProperty("vm_version")]
        public byte VmVersion;

        // abi-field-name: code_hash ,abi-field-type: checksum256
        [JsonProperty("code_hash")]
        public Checksum256 CodeHash;

        public CodeId(byte vmType, byte vmVersion, string codeHash)
        {
            this.VmType = vmType;
            this.VmVersion = vmVersion;
            this.CodeHash = codeHash;
        }

        public CodeId()
        {
        }
    }
}