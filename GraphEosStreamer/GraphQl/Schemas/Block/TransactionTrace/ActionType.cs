using GraphQL.Types;
using Action = GraphEosStreamer.SHiP.Action;

namespace GraphEosStreamer.GraphQl.Schemas.Block.TransactionTrace
{
    public class ActionType : ObjectGraphType<Action>
    {
        public ActionType()
        {
            Field("Name", o => o.Name.ToJson()).Returns<string>();
            Field("Account",o => o.Account.ToJson()).Returns<string>();
            Field("Data", o => o.Data.ToJson()).Returns<string>();
            Field(o => o.Authorization, true);
        }
    }
}
