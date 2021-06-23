using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block.SignedBlock
{
    public class ProducerKeyType : ObjectGraphType<SHiP.ProducerKey>
    {
        public ProducerKeyType()
        {
            Field("BlockSigningKey",o => o.BlockSigningKey.ToJson()).Returns<string>();
            Field("ProducerName",o => o.ProducerName.ToJson()).Returns<string>();
        }
    }
}
