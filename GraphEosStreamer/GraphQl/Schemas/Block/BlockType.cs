using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block
{
    public class BlockType : ObjectGraphType<Block>
    {
        public BlockType()
        {
            Field(o => o.SignedBlock);
            Field(o => o.TransactionTraces);
            Field(o => o.TableDeltas);
//            Field(o => o.From);//, false, typeof(MessageFromType)).Resolve(ResolveFrom);

        }

/*        private MessageFrom ResolveFrom(IResolveFieldContext<Block> context)
        {
            var message = context.Source;
            return message.From;
        }*/
    }
}
