using GraphEosStreamer.SHiP.Variants;
using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block.TableDelta
{
    public class TableDeltaV0Type : ObjectGraphType<TableDeltaV0>
    {
        public TableDeltaV0Type()
        {
            Field(o => o.Name);
            Field(o => o.Rows, true);
        }
    }
}
