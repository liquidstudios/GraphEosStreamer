using GraphQL.Types;

namespace GraphEosStreamer.GraphQl.Schemas.Block.SignedBlock
{
    public class ProducerScheduleType : ObjectGraphType<SHiP.ProducerSchedule>
    {
        public ProducerScheduleType()
        {
            Field(o => o.Producers);
            Field(o => o.Version);
        }
    }
}
