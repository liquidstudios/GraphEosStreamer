using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block.SignedBlock
{
    public class SignedBlockType : ObjectGraphType<SHiP.SignedBlock>
    {
        public SignedBlockType()
        {
            Field(o => o.BlockExtensions,true);
            Field(o => o.Transactions, true);//, type: typeof(DateGraphType));
            Field("ActionMroot",o => o.ActionMroot.ToJson()).Returns<string>();
            Field(o => o.Confirmed);
            Field(o => o.HeaderExtensions, true);
            Field(o => o.NewProducers, true);
            Field("Previous",o => o.Previous.ToJson()).Returns<string>();
            Field("Producer",o => o.Producer.ToJson()).Returns<string>();
            Field("ProducerSignature",o => o.ProducerSignature.ToJson()).Returns<string>();
            Field(o => o.ScheduleVersion);
            Field(o => o.Timestamp);
            Field("TransactionMroot",o => o.TransactionMroot.ToJson()).Returns<string>();
            //            Field(o => o.From);//, false, typeof(MessageFromType)).Resolve(ResolveFrom);
        }
    }
}
