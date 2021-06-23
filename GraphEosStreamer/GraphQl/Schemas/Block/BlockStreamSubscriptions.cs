using System;
using System.Linq;
using System.Security.Claims;
using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Server.Transports.Subscriptions.Abstractions;
using GraphQL.Subscription;
using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block
{
    public class BlockStreamSubscriptions : ObjectGraphType<object>
    {
        private readonly IBlockStream _blockStream;

        public BlockStreamSubscriptions(IBlockStream blockStream)
        {
            _blockStream = blockStream;
            AddField(new EventStreamFieldType
            {
                Name = "onBlock",
                Type = typeof(BlockType),
                Resolver = new FuncFieldResolver<Block>(ResolveMessage),
                Subscriber = new EventStreamResolver<Block>(Subscribe)
            });

/*            AddField(new EventStreamFieldType
            {
                Name = "messageAddedByUser",
                Arguments = new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }
                ),
                Type = typeof(MessageType),
                Resolver = new FuncFieldResolver<Block>(ResolveMessage),
                Subscriber = new EventStreamResolver<Block>(SubscribeById)
            });*/
        }

        private IObservable<Block> SubscribeById(IResolveEventStreamContext context)
        {
            var messageContext = (MessageHandlingContext)context.UserContext;
            var user = messageContext.Get<ClaimsPrincipal>("user");

            string sub = "Anonymous";
            if (user != null)
                sub = user.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            var blocks = _blockStream.Blocks(sub);

            string id = context.GetArgument<string>("id");
            return blocks;//.Where(message => message.From.Id == id);
        }

        private Block ResolveMessage(IResolveFieldContext context)
        {
            var message = context.Source as Block;

            return message;
        }

        private IObservable<Block> Subscribe(IResolveEventStreamContext context)
        {
            var messageContext = (MessageHandlingContext)context.UserContext;
            var user = messageContext.Get<ClaimsPrincipal>("user");

            string sub = "Anonymous";
            if (user != null)
                sub = user.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;

            return _blockStream.Blocks(sub);
        }
    }
}
