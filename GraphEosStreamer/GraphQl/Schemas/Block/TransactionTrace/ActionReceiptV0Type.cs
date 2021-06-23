using GraphEosStreamer.SHiP.Variants;
using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block.TransactionTrace
{
    public class ActionReceiptV0Type : ObjectGraphType<ActionReceiptV0>
    {
        public ActionReceiptV0Type()
        {
            Field("AbiSequence", o => o.AbiSequence.ToUint()).Returns<uint>();
            Field("ActDigest", o => o.ActDigest.ToJson()).Returns<string>();
            Field("CodeSequence",o => o.CodeSequence.ToUint()).Returns<uint>();
            Field(o => o.GlobalSequence);
            Field("Receiver", o => o.Receiver.ToJson()).Returns<string>();
            Field(o => o.RecvSequence);

            Field(o => o.AuthSequence);
        }
    }
}
