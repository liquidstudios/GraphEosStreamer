using System;
using GraphEosStreamer.SHiP.EosTypes;
using Newtonsoft.Json;

namespace GraphEosStreamer.AssemblyGenerator
{
    [Serializable]
    public class Abi
    {
        public string Version;
        public AbiType[] AbiTypes;
        public AbiStruct[] AbiStructs;
        public AbiAction[] AbiActions;
        public AbiTable[] AbiTables;
    }

    [Serializable]
    public class AbiType
    {
        [JsonProperty("new_type_name")]
        public string NewTypeName;
        
        [JsonProperty("type")]
        public string Type;
    }

    [Serializable]
    public class AbiStruct
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("base")]
        public string Base;

        [JsonProperty("fields")]
        public AbiField[] Fields;
    }

    [Serializable]
    public class AbiField
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("type")]
        public string Type;
    }

    [Serializable]
    public class AbiAction
    {
        [JsonProperty("name")]
        public Name Name;

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("ricardian_contract")]
        public string RicardianContract;
    }

    [Serializable]
    public class AbiTable
    {
        [JsonProperty("name")]
        public Name Name;

        [JsonProperty("index_type")] 
        public string IndexType;

        [JsonProperty("key_names")] 
        public string[] KeyNames;

        [JsonProperty("key_types")] 
        public string[] KeyTypes;

        [JsonProperty("type")] 
        public string Type;
    }
}
