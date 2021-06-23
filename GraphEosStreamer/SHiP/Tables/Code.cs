using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.Variants;


namespace GraphEosStreamer.SHiP.Tables
{
    public abstract class Code : Table
    {

    }

    [Serializable()]
    public class CodeV0 : Code
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

        // abi-field-name: code ,abi-field-type: bytes
        [JsonProperty("code")]
        public Bytes Code;

        public CodeV0(byte vmType, byte vmVersion, string codeHash, byte[] code)
        {
            this.VmType = vmType;
            this.VmVersion = vmVersion;
            this.CodeHash = codeHash;
            this.Code = code;
        }

        public CodeV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}