using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.EosTypes;

namespace GraphEosStreamer.SHiP
{
    [Serializable()]
    public class AccountDelta
    {

        // abi-field-name: account ,abi-field-type: name
        [JsonProperty("account")]
        public Name Account;

        // abi-field-name: delta ,abi-field-type: int64
        [JsonProperty("delta")]
        public long Delta;

        public AccountDelta(Name account, long delta)
        {
            this.Account = account;
            this.Delta = delta;
        }

        public AccountDelta()
        {
        }
    }
}