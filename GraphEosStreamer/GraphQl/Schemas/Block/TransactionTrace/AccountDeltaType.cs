using GraphEosStreamer.SHiP;
using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block.TransactionTrace
{
    public class AccountDeltaType : ObjectGraphType<AccountDelta>
    {
        public AccountDeltaType()
        {
            Field("Account", o => o.Account.ToJson()).Returns<string>();
            Field(o => o.Delta);
        }
    }
}
