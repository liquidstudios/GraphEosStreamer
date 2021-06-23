using System;
using Newtonsoft.Json;
using GraphEosStreamer.Other;

namespace GraphEosStreamer.SHiP
{
    [Serializable()]
    public class SignedBlock : SignedBlockHeader
    {

        // abi-field-name: transactions ,abi-field-type: transaction_receipt[]
        [SortOrder(11)]
        [JsonProperty("transactions")]
        public TransactionReceipt[] Transactions;

        // abi-field-name: block_extensions ,abi-field-type: extension[]
        [SortOrder(12)]
        [JsonProperty("block_extensions")]
        public Extension[] BlockExtensions;

        public SignedBlock(TransactionReceipt[] transactions, Extension[] blockExtensions)
        {
            this.Transactions = transactions;
            this.BlockExtensions = blockExtensions;
        }

        public SignedBlock()
        {
        }
    }
}