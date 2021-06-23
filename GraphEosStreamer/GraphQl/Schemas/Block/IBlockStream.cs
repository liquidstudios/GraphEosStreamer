using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace GraphEosStreamer.GraphQl.Schemas.Block
{
    public interface IBlockStream
    {
        ConcurrentStack<Block> AllBlocks { get; }

        Block AddBlock(Block block);

        IObservable<Block> Blocks(string user);
    }

    public class BlockStream : IBlockStream {

        private readonly ISubject<Block> _blockStream = new ReplaySubject<Block>(1);

        public BlockStream()
        {
            AllBlocks = new ConcurrentStack<Block>();
        }

        public ConcurrentStack<Block> AllBlocks { get; }

        public Block AddBlock(Block block)
        {
            AllBlocks.Push(block);
            // TODO inject number of blocks to hold, pop if AllBlocks.Lenght >= num
            AllBlocks.TryPop(out var poppedBlock);

            _blockStream.OnNext(block);
            return block;
        }

        public IObservable<Block> Blocks(string user)
        {
            return _blockStream
                .Select(message =>
                {
//                    message.Sub = user;
                    return message;
                })
                .AsObservable();
        }

        public void AddError(Exception exception) => _blockStream.OnError(exception);
    }
}
