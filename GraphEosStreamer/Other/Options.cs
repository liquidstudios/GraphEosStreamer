using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphEosStreamer
{
    public class Options
    {
        public uint StartBlockNum { get; set; }
        public uint EndBlockNum { get; set; }
        public uint MaxMessagesInFlight { get; set; }

        public int DeltaDeserializerTasks { get; set; }
        public int BlockDeserializerTasks { get; set; }
        public string ShipUrl { get; set; }
        
        public SortedSet<string> IgnoreContracts { get; set; }

        public bool StreamBlocks { get; set; }
        public bool MergeDeltas { get; set; }
    }
}
