using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;

namespace GraphEosStreamer.SHiP
{
    [Serializable()]
    public class AccountAuthSequence
    {

        // abi-field-name: account ,abi-field-type: name
        [JsonProperty("account")]
        public Name Account;

        // abi-field-name: sequence ,abi-field-type: uint64
        [JsonProperty("sequence")]
        public ulong Sequence;

        public AccountAuthSequence(Name account, ulong sequence)
        {
            this.Account = account;
            this.Sequence = sequence;
        }

        public AccountAuthSequence()
        {
        }
    }
}