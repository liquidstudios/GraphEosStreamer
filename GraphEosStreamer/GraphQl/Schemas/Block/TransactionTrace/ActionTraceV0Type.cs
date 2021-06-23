using GraphEosStreamer.SHiP.Variants;
using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block.TransactionTrace
{
    public class ActionTraceV0Type : ObjectGraphType<ActionTraceV0>
    {
        public ActionTraceV0Type()
        {
            Field("ActionOrdinal",o => o.ActionOrdinal.ToUint()).Returns<uint>();
            Field(o => o.Console, true);
            Field(o => o.ContextFree);
            Field("CreatorActionOrdinal",o => o.CreatorActionOrdinal.ToUint()).Returns<uint>();
            Field(o => o.Elapsed);
            Field(o => o.ErrorCode, true);
            Field(o => o.Except, true);
            Field("Receiver",o => o.Receiver.ToJson());

            Field(o => o.AccountRamDeltas, true);
            Field("Receipt", o => ((ActionReceiptV0?)o.Receipt), true).Returns<ActionReceiptV0>();
            Field(o => o.Act);
        }
    }
}
