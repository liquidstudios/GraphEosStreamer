using System.Collections.Generic;
using System.Linq;
using GraphEosStreamer.SHiP.Variants;
using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block.TransactionTrace
{
    public class PartialTransactionV0Type : ObjectGraphType<PartialTransactionV0>
    {
        public PartialTransactionV0Type()
        {
            Field("Signatures",o => o.Signatures.Select( s => s.ToJson())).Returns<List<string>>();
            Field("ContextFreeData",o => o.ContextFreeData.ToJson()).Returns<string>();
            Field("DelaySec", o => o.DelaySec.ToUint()).Returns<uint>();
            Field(o => o.Expiration);
            Field(o => o.MaxCpuUsageMs);
            Field("MaxNetUsageWords",o => o.MaxNetUsageWords.ToUint()).Returns<uint>();
            Field(o => o.RefBlockNum);
            Field(o => o.TransactionExtensions, true);
        }
    }
}
