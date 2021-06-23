using System.Linq;
using GraphEosStreamer.SHiP.Variants;

namespace GraphEosStreamer.GraphQl.Schemas.Block
{
    public class Block
    {
        public SHiP.SignedBlock SignedBlock { get; set; }

        public TransactionTraceV0[] TransactionTraces { get; set; }

        public TableDeltaV0[] TableDeltas { get; set; }

        public Block(GetBlocksResultV0 blockResult)
        {
            this.SignedBlock = blockResult.BlockBytes?.Instance;
            this.TransactionTraces = blockResult.TracesBytes?.Instance.Select(t => (TransactionTraceV0)t).ToArray();
            this.TableDeltas = blockResult.DeltasBytes?.Instance.Select(t => (TableDeltaV0)t).ToArray();
        }
    }
}
