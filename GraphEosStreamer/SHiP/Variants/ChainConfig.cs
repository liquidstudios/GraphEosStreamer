using System;
using Newtonsoft.Json;


namespace GraphEosStreamer.SHiP.Variants
{
    public abstract class ChainConfig
    {
        public abstract string ToJson();
    }


    [Serializable()]
    public class ChainConfigV0 : ChainConfig
    {

        // abi-field-name: max_block_net_usage ,abi-field-type: uint64
        [JsonProperty("max_block_net_usage")]
        public ulong MaxBlockNetUsage;

        // abi-field-name: target_block_net_usage_pct ,abi-field-type: uint32
        [JsonProperty("target_block_net_usage_pct")]
        public uint TargetBlockNetUsagePct;

        // abi-field-name: max_transaction_net_usage ,abi-field-type: uint32
        [JsonProperty("max_transaction_net_usage")]
        public uint MaxTransactionNetUsage;

        // abi-field-name: base_per_transaction_net_usage ,abi-field-type: uint32
        [JsonProperty("base_per_transaction_net_usage")]
        public uint BasePerTransactionNetUsage;

        // abi-field-name: net_usage_leeway ,abi-field-type: uint32
        [JsonProperty("net_usage_leeway")]
        public uint NetUsageLeeway;

        // abi-field-name: context_free_discount_net_usage_num ,abi-field-type: uint32
        [JsonProperty("context_free_discount_net_usage_num")]
        public uint ContextFreeDiscountNetUsageNum;

        // abi-field-name: context_free_discount_net_usage_den ,abi-field-type: uint32
        [JsonProperty("context_free_discount_net_usage_den")]
        public uint ContextFreeDiscountNetUsageDen;

        // abi-field-name: max_block_cpu_usage ,abi-field-type: uint32
        [JsonProperty("max_block_cpu_usage")]
        public uint MaxBlockCpuUsage;

        // abi-field-name: target_block_cpu_usage_pct ,abi-field-type: uint32
        [JsonProperty("target_block_cpu_usage_pct")]
        public uint TargetBlockCpuUsagePct;

        // abi-field-name: max_transaction_cpu_usage ,abi-field-type: uint32
        [JsonProperty("max_transaction_cpu_usage")]
        public uint MaxTransactionCpuUsage;

        // abi-field-name: min_transaction_cpu_usage ,abi-field-type: uint32
        [JsonProperty("min_transaction_cpu_usage")]
        public uint MinTransactionCpuUsage;

        // abi-field-name: max_transaction_lifetime ,abi-field-type: uint32
        [JsonProperty("max_transaction_lifetime")]
        public uint MaxTransactionLifetime;

        // abi-field-name: deferred_trx_expiration_window ,abi-field-type: uint32
        [JsonProperty("deferred_trx_expiration_window")]
        public uint DeferredTrxExpirationWindow;

        // abi-field-name: max_transaction_delay ,abi-field-type: uint32
        [JsonProperty("max_transaction_delay")]
        public uint MaxTransactionDelay;

        // abi-field-name: max_inline_action_size ,abi-field-type: uint32
        [JsonProperty("max_inline_action_size")]
        public uint MaxInlineActionSize;

        // abi-field-name: max_inline_action_depth ,abi-field-type: uint16
        [JsonProperty("max_inline_action_depth")]
        public ushort MaxInlineActionDepth;

        // abi-field-name: max_authority_depth ,abi-field-type: uint16
        [JsonProperty("max_authority_depth")]
        public ushort MaxAuthorityDepth;

        public ChainConfigV0(
                    ulong maxBlockNetUsage,
                    uint targetBlockNetUsagePct,
                    uint maxTransactionNetUsage,
                    uint basePerTransactionNetUsage,
                    uint netUsageLeeway,
                    uint contextFreeDiscountNetUsageNum,
                    uint contextFreeDiscountNetUsageDen,
                    uint maxBlockCpuUsage,
                    uint targetBlockCpuUsagePct,
                    uint maxTransactionCpuUsage,
                    uint minTransactionCpuUsage,
                    uint maxTransactionLifetime,
                    uint deferredTrxExpirationWindow,
                    uint maxTransactionDelay,
                    uint maxInlineActionSize,
                    ushort maxInlineActionDepth,
                    ushort maxAuthorityDepth)
        {
            this.MaxBlockNetUsage = maxBlockNetUsage;
            this.TargetBlockNetUsagePct = targetBlockNetUsagePct;
            this.MaxTransactionNetUsage = maxTransactionNetUsage;
            this.BasePerTransactionNetUsage = basePerTransactionNetUsage;
            this.NetUsageLeeway = netUsageLeeway;
            this.ContextFreeDiscountNetUsageNum = contextFreeDiscountNetUsageNum;
            this.ContextFreeDiscountNetUsageDen = contextFreeDiscountNetUsageDen;
            this.MaxBlockCpuUsage = maxBlockCpuUsage;
            this.TargetBlockCpuUsagePct = targetBlockCpuUsagePct;
            this.MaxTransactionCpuUsage = maxTransactionCpuUsage;
            this.MinTransactionCpuUsage = minTransactionCpuUsage;
            this.MaxTransactionLifetime = maxTransactionLifetime;
            this.DeferredTrxExpirationWindow = deferredTrxExpirationWindow;
            this.MaxTransactionDelay = maxTransactionDelay;
            this.MaxInlineActionSize = maxInlineActionSize;
            this.MaxInlineActionDepth = maxInlineActionDepth;
            this.MaxAuthorityDepth = maxAuthorityDepth;
        }

        public ChainConfigV0()
        {
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}