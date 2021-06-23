using GraphEosStreamer.SHiP;
using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block.TransactionTrace
{
    public class PermissionLevelType : ObjectGraphType<PermissionLevel>
    {
        public PermissionLevelType()
        {
            Field("Actor",o => o.Actor.ToJson()).Returns<string>();
            Field("Permission", o => o.Permission.ToJson()).Returns<string>();
        }
    }
}
