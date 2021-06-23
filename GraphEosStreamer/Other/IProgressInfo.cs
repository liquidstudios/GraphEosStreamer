namespace GraphEosStreamer.Other
{
    public interface IProgressInfo
    {
        uint LastSynchedBlock { get; set; }
    }

    public class ProgressInfo : IProgressInfo
    {
        public uint LastSynchedBlock { get; set; }
    }
}
