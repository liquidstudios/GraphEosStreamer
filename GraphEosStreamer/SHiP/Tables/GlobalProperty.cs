using System;
using Newtonsoft.Json;
using GraphEosStreamer.SHiP.Variants;


namespace GraphEosStreamer.SHiP.Tables
{
    public abstract class GlobalProperty : Table
    {

    }

    [Serializable()]
    public class GlobalPropertyV0 : GlobalProperty
    {

        // abi-field-name: proposed_schedule_block_num ,abi-field-type: uint32?
        [JsonProperty("proposed_schedule_block_num")]
        public uint? ProposedScheduleBlockNum;

        // abi-field-name: proposed_schedule ,abi-field-type: producer_schedule
        [JsonProperty("proposed_schedule")]
        public ProducerSchedule ProposedSchedule;

        // abi-field-name: configuration ,abi-field-type: chain_config
        [JsonProperty("configuration")]
        public ChainConfig Configuration;

        public GlobalPropertyV0(uint? proposedScheduleBlockNum, ProducerSchedule proposedSchedule, ChainConfig configuration)
        {
            this.ProposedScheduleBlockNum = proposedScheduleBlockNum;
            this.ProposedSchedule = proposedSchedule;
            this.Configuration = configuration;
        }

        public GlobalPropertyV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [Serializable()]
    public class GlobalPropertyV1 : GlobalProperty
    {

        // abi-field-name: proposed_schedule_block_num ,abi-field-type: uint32?
        [JsonProperty("proposed_schedule_block_num")]
        public uint? ProposedScheduleBlockNum;

        // abi-field-name: proposed_schedule ,abi-field-type: producer_authority_schedule
        [JsonProperty("proposed_schedule")]
        public ProducerAuthoritySchedule ProposedSchedule;

        // abi-field-name: configuration ,abi-field-type: chain_config
        [JsonProperty("configuration")]
        public ChainConfig Configuration;

        // abi-field-name: chain_id ,abi-field-type: checksum256
        [JsonProperty("chain_id")]
        public Checksum256 ChainId;

        public GlobalPropertyV1(uint? proposedScheduleBlockNum, ProducerAuthoritySchedule proposedSchedule, ChainConfig configuration, string chainId)
        {
            this.ProposedScheduleBlockNum = proposedScheduleBlockNum;
            this.ProposedSchedule = proposedSchedule;
            this.Configuration = configuration;
            this.ChainId = chainId;
        }

        public GlobalPropertyV1()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}