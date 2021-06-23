using System;
using Newtonsoft.Json;
using GraphEosStreamer.Other;
using GraphEosStreamer.SHiP.Variants;

namespace GraphEosStreamer.SHiP
{
    [Serializable()]
    public class TransactionReceipt : TransactionReceiptHeader
    {

        // abi-field-name: trx ,abi-field-type: transaction_variant
        [SortOrder(4)]
        [JsonProperty("trx")]
        public TransactionVariant Trx;

        public TransactionReceipt(TransactionVariant trx)
        {
            this.Trx = trx;
        }

        public TransactionReceipt()
        {
        }
    }
}