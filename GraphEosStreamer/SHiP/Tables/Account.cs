using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;


namespace GraphEosStreamer.SHiP.Tables
{
    public abstract class Account : Table
    {

    }

    [Serializable()]
    public class AccountV0 : Account
    {

        // abi-field-name: name ,abi-field-type: name
        [JsonProperty("name")]
        public Name Name;

        // abi-field-name: creation_date ,abi-field-type: block_timestamp_type
        [JsonProperty("creation_date")]
        public uint CreationDate;

        // abi-field-name: abi ,abi-field-type: bytes
        [JsonProperty("abi")]
        public Bytes Abi;

        public AccountV0(Name name, uint creationDate, byte[] abi)
        {
            this.Name = name;
            this.CreationDate = creationDate;
            this.Abi = abi;
        }

        public AccountV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}