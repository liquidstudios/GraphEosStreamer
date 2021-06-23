using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block.SignedBlock
{
    public class PackedTransactionVariantType : ObjectGraphType<SHiP.Variants.PackedTransactionVariant>
    {
        public PackedTransactionVariantType()
        {
            Field("PackedTrx", o => o.PackedTrx.ToJson(), true).Returns<string>();
            Field(o => o.Compression);
            Field("PackedContextFreeData",o => o.PackedContextFreeData.ToJson()).Returns<string>();
            Field(o => o.Signatures);
        }
    }
}
