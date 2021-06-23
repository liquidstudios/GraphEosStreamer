using System.Linq;
using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block
{
    public class BlockStreamQuery : ObjectGraphType
    {
        public BlockStreamQuery(IBlockStream blockStream)
        {
            Field<ListGraphType<BlockType>>("blocks", resolve: context => blockStream.AllBlocks.Take(100));
        }
    }
}
