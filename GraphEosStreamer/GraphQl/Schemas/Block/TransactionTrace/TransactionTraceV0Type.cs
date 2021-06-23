using System.Linq;
using GraphEosStreamer.SHiP.Variants;
using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block.TransactionTrace
{
    public class TransactionTraceV0Type : ObjectGraphType<TransactionTraceV0>
    {
        public TransactionTraceV0Type()
        {
            
            Field(o => o.CpuUsageUs);
            Field(o => o.Elapsed);
            Field(o => o.ErrorCode, true);
            Field(o => o.Except, true);
            Field("Id",o => o.Id.ToJson()).Returns<string>();
            Field(o => o.NetUsage);
            Field("NetUsageWords",o => o.NetUsageWords.ToUint()).Returns<uint>();
            Field(o => o.Scheduled);
            Field(o => o.Status);
            
            Field("FailedDtrxTrace",o => ((TransactionTraceV0?)o.FailedDtrxTrace), true).Returns<TransactionTraceV0>(); 
            Field("Partial",o => ((PartialTransactionV0?)o.Partial),true).Returns<PartialTransactionV0>();
            
            Field("ActionTraces",o => o.ActionTraces.OfType<ActionTraceV0>(), true).Returns<ActionTraceV0[]>();
            Field(o => o.AccountRamDelta, true);
        }
    }
}
