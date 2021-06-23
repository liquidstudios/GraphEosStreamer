using GraphEosStreamer.Other;
using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block.SignedBlock
{
    public class ExtensionType : ObjectGraphType<SHiP.Extension>
    {
        public ExtensionType()
        {
            Field(o => o.Type);
            Field("data", o => SerializationHelper.ByteArrayToHexString(o.Data)).Returns<string>();
        }
    }
}
