using System;
using Newtonsoft.Json;
using GraphEosStreamer.Other;
using GraphEosStreamer.SHiP.EosTypes;
using GraphEosStreamer.SHiP.Variants;

namespace GraphEosStreamer.SHiP
{
    [Serializable()]
    public class BlockHeader
    {

        // abi-field-name: timestamp ,abi-field-type: block_timestamp_type
        [SortOrder(1)]
        [JsonProperty("timestamp")]
        public uint Timestamp;

        // abi-field-name: producer ,abi-field-type: name
        [SortOrder(2)]
        [JsonProperty("producer")]
        public Name Producer;

        // abi-field-name: confirmed ,abi-field-type: uint16
        [SortOrder(3)]
        [JsonProperty("confirmed")]
        public ushort Confirmed;

        // abi-field-name: previous ,abi-field-type: checksum256
        [SortOrder(4)]
        [JsonProperty("previous")]
        public Checksum256 Previous;

        // abi-field-name: transaction_mroot ,abi-field-type: checksum256
        [SortOrder(5)]
        [JsonProperty("transaction_mroot")]
        public Checksum256 TransactionMroot;

        // abi-field-name: action_mroot ,abi-field-type: checksum256
        [SortOrder(6)]
        [JsonProperty("action_mroot")]
        public Checksum256 ActionMroot;

        // abi-field-name: schedule_version ,abi-field-type: uint32
        [SortOrder(7)]
        [JsonProperty("schedule_version")]
        public uint ScheduleVersion;

        // abi-field-name: new_producers ,abi-field-type: producer_schedule?
        [SortOrder(8)]
        [JsonProperty("new_producers")]
        public ProducerSchedule? NewProducers;

        // abi-field-name: header_extensions ,abi-field-type: extension[]
        [SortOrder(9)]
        [JsonProperty("header_extensions")]
        public Extension[] HeaderExtensions;

        public BlockHeader(uint timestamp, Name producer, ushort confirmed, string previous, string transactionMroot, string actionMroot, uint scheduleVersion, ProducerSchedule? newProducers, Extension[] headerExtensions)
        {
            this.Timestamp = timestamp;
            this.Producer = producer;
            this.Confirmed = confirmed;
            this.Previous = previous;
            this.TransactionMroot = transactionMroot;
            this.ActionMroot = actionMroot;
            this.ScheduleVersion = scheduleVersion;
            this.NewProducers = newProducers;
            this.HeaderExtensions = headerExtensions;
        }

        public BlockHeader()
        {
        }
    }
}