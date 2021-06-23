using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block
{
    public class BlockStreamMutation : ObjectGraphType<object>
    {
        public BlockStreamMutation(IBlockStream chat)
        {
/*            Field<MessageType>("addMessage",
                arguments: new QueryArguments(
                    new QueryArgument<MessageInputType> { Name = "message" }
                ),
                resolve: context =>
                {
                    var receivedMessage = context.GetArgument<ReceivedMessage>("message");
                    var message = chat.AddMessage(receivedMessage);
                    return message;
                });*/
        }
    }

/*    public class MessageInputType : InputObjectGraphType
    {
        public MessageInputType()
        {
            Field<StringGraphType>("fromId");
            Field<StringGraphType>("content");
            Field<DateGraphType>("sentAt");
        }
    }*/
}
