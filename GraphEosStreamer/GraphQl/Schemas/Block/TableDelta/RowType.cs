
using GraphEosStreamer.Other;
using GraphEosStreamer.SHiP;
using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block.TableDelta
{
    public class RowType : ObjectGraphType<Row>
    {
        public RowType()
        {
            Field(o => o.Present);
//            Field("Data",o => SerializationHelper.ByteArrayToHexString( o.Data)).Returns<string>();
            Field("Data", o => o.Table != null ? o.Table.ToJson() : SerializationHelper.ByteArrayToHexString(o.Data),
                true).Returns<string>();
        }
    }
}
