using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block.SignedBlock
{
    public class TransactionReceiptType : ObjectGraphType<SHiP.TransactionReceipt>
    {
        public TransactionReceiptType()
        {
            Field("trx",o => o.Trx.ToJson()).Returns<string>();
            Field(o => o.CpuUsageUs);
            Field("NetUsageWords",o => o.NetUsageWords.ToUint()).Returns<uint>();
            Field(o => o.Status);
        }
    }
}
