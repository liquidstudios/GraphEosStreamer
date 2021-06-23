using System;
using GraphEosStreamer.GraphQl.Schemas.Block.SignedBlock;
using GraphEosStreamer.GraphQl.Schemas.Block.TableDelta;
using GraphEosStreamer.GraphQl.Schemas.Block.TransactionTrace;
using GraphQL;
using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block
{
    public class BlockStreamSchema : Schema
    {
        public BlockStreamSchema(IBlockStream chat, IServiceProvider provider) : base(provider)
        {
            Query = new BlockStreamQuery(chat);
//            Mutation = new BlockStreamMutation(chat);
            Subscription = new BlockStreamSubscriptions(chat);

            this.RegisterTypeMapping<SHiP.SignedBlock, SignedBlockType>();
            this.RegisterTypeMapping<SHiP.Extension, ExtensionType>();
            this.RegisterTypeMapping<SHiP.TransactionReceipt, TransactionReceiptType>();
            this.RegisterTypeMapping<SHiP.ProducerSchedule, ProducerScheduleType>();
            this.RegisterTypeMapping<SHiP.ProducerKey, ProducerKeyType>();

            this.RegisterTypeMapping<SHiP.AccountAuthSequence, AccountAuthSequenceType>();
            this.RegisterTypeMapping<SHiP.AccountDelta, AccountDeltaType>();
            this.RegisterTypeMapping<SHiP.Variants.ActionReceiptV0, ActionReceiptV0Type>();
            this.RegisterTypeMapping<SHiP.Variants.ActionTraceV0, ActionTraceV0Type>(); 
            this.RegisterTypeMapping<SHiP.Variants.TransactionTraceV0, TransactionTraceV0Type>();
            this.RegisterTypeMapping<SHiP.Variants.PartialTransactionV0, PartialTransactionV0Type>();
            this.RegisterTypeMapping<SHiP.PermissionLevel, PermissionLevelType>();
            this.RegisterTypeMapping<SHiP.Action, ActionType>();

            this.RegisterTypeMapping<SHiP.Variants.TableDeltaV0, TableDeltaV0Type>();
            this.RegisterTypeMapping<SHiP.Row, RowType>();
        }
    }
}
