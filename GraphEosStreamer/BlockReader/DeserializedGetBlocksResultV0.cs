using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphEosStreamer.SHiP.Variants;

namespace GraphEosStreamer.BlockReader
{
    public class DeserializedGetBlocksResultV0
    {
        public GetBlocksResultV0 GetBlocksResultV0;

        public DeserializedGetBlocksResultV0(GetBlocksResultV0 getBlocksResultV0)
        {
            GetBlocksResultV0 = getBlocksResultV0;
        }
    }
}
