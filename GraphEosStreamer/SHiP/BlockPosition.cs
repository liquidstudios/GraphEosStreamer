using Newtonsoft.Json;
using GraphEosStreamer.SHiP.Variants;

namespace GraphEosStreamer.SHiP
{
    public class BlockPosition
    {

        // abi-field-name: block_num ,abi-field-type: uint32
        [JsonProperty("block_num")]
        public uint BlockNum;

        // abi-field-name: block_id ,abi-field-type: checksum256
        [JsonProperty("block_id")]
        public Checksum256 BlockId;

        public BlockPosition(uint blockNum, string blockId)
        {
            this.BlockNum = blockNum;
            this.BlockId = blockId;
        }

        public BlockPosition()
        {
        }
    }
}
