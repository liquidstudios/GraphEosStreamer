using GraphEosStreamer.SHiP;
using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block.TransactionTrace
{
    public class AccountAuthSequenceType : ObjectGraphType<AccountAuthSequence>
    {
        public AccountAuthSequenceType()
        {
            Field("Account",o => o.Account.ToJson()).Returns<string>();
            Field(o => o.Sequence);
        }
    }
}
