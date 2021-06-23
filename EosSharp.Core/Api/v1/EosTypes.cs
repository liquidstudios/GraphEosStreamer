  

  

// Auto Generated, do not edit.
using EosSharp.Core.DataAttributes;
using System;
using System.Collections.Generic;

namespace EosSharp.Core.Api.v1
{
	#region generate api types

	public class Symbol
    {
		
		public string name;
		
		public byte precision;
    }

	public class Resource
    {
		
		public Int64 used;
		
		public Int64 available;
		
		public Int64 max;
    }

	public class AuthorityKey
    {
		
		public string key;
		
		public Int32 weight;
    }

	public class AuthorityAccount
    {
		
		public PermissionLevel permission;
		
		public Int32 weight;
    }

	public class AuthorityWait
    {
		
		public string wait_sec;
		
		public Int32 weight;
    }

	public class Authority
    {
		
		public UInt32 threshold;
		
		public List<AuthorityKey> keys;
		
		public List<AuthorityAccount> accounts;
		
		public List<AuthorityWait> waits;
    }

	public class Permission
    {
		
		public string perm_name;
		
		public string parent;
		
		public Authority required_auth;
    }

	public class AbiType
    {
		
		public string new_type_name;
		
		public string type;
    }

	public class AbiField
    {
		
		public string name;
		
		public string type;
    }

	public class AbiStruct
    {
		
		public string name;
		
		public string @base;
		
		public List<AbiField> fields;
    }

	public class AbiAction
    {
		[AbiFieldType("name")]
		public string name;
		
		public string type;
		
		public string ricardian_contract;
    }

	public class AbiTable
    {
		[AbiFieldType("name")]
		public string name;
		
		public string index_type;
		
		public List<string> key_names;
		
		public List<string> key_types;
		
		public string type;
    }

	public class Abi
    {
		
		public string version;
		
		public List<AbiType> types;
		
		public List<AbiStruct> structs;
		
		public List<AbiAction> actions;
		
		public List<AbiTable> tables;
		
		public List<AbiRicardianClause> ricardian_clauses;
		
		public List<string> error_messages;
		
		public List<Extension> abi_extensions;
		
		public List<Variant> variants;
    }

	public class AbiRicardianClause
    {
		
		public string id;
		
		public string body;
    }

	public class CurrencyStat
    {
		
		public string supply;
		
		public string max_supply;
		
		public string issuer;
    }

	public class Producer
    {
		[AbiFieldType("name")]
		public string owner;
		[AbiFieldType("float64")]
		public double total_votes;
		[AbiFieldType("public_key")]
		public string producer_key;
		
		public bool is_active;
		
		public string url;
		
		public UInt32 unpaid_blocks;
		
		public UInt64 last_claim_time;
		
		public UInt16 location;
    }

	public class ScheduleProducers
    {
		
		public string producer_name;
		
		public string block_signing_key;
    }

	public class Schedule
    {
		
		public UInt32? version;
		
		public List<ScheduleProducers> producers;
    }

	public class PermissionLevel
    {
		
		public string actor;
		
		public string permission;
    }

	public class Extension
    {
		
		public UInt16 type;
		
		public byte[] data;
    }

	public class Variant
    {
		
		public string name;
		
		public List<string> types;
    }

	public class Action
    {
		
		public string account;
		
		public string name;
		
		public List<PermissionLevel> authorization;
		
		public object data;
		
		public string hex_data;
    }

	public class Transaction
    {
		
		public DateTime expiration;
		
		public UInt16 ref_block_num;
		
		public UInt32 ref_block_prefix;
		
		public UInt32 max_net_usage_words;
		
		public byte max_cpu_usage_ms;
		
		public UInt32 delay_sec;
		
		public List<Action> context_free_actions = new List<Action>();
		
		public List<Action> actions = new List<Action>();
		
		public List<Extension> transaction_extensions = new List<Extension>();
    }

	public class ScheduledTransaction
    {
		
		public string trx_id;
		
		public string sender;
		
		public string sender_id;
		
		public string payer;
		
		public DateTime? delay_until;
		
		public DateTime? expiration;
		
		public DateTime? published;
		
		public Object transaction;
    }

	public class Receipt
    {
		
		public string receiver;
		
		public string act_digest;
		
		public UInt64? global_sequence;
		
		public UInt64? recv_sequence;
		
		public List<List<object>> auth_sequence;
		
		public UInt64? code_sequence;
		
		public UInt64? abi_sequence;
    }

	public class ActionTrace
    {
		
		public Receipt receipt;
		
		public Action act;
		
		public UInt32? elapsed;
		
		public UInt32? cpu_usage;
		
		public string console;
		
		public UInt32? total_cpu_usage;
		
		public string trx_id;
		
		public List<ActionTrace> inline_traces;
    }

	public class GlobalAction
    {
		
		public UInt64? global_action_seq;
		
		public UInt64? account_action_seq;
		
		public UInt32? block_num;
		
		public DateTime? block_time;
		
		public ActionTrace action_trace;
    }

	public class TransactionReceipt
    {
		
		public string status;
		
		public UInt32? cpu_usage_us;
		
		public UInt32? net_usage_words;
		
		public object trx;
    }

	public class ProcessedTransaction
    {
		
		public string id;
		
		public TransactionReceipt receipt;
		
		public UInt32 elapsed;
		
		public UInt32 net_usage;
		
		public bool scheduled;
		
		public string except;
		
		public List<ActionTrace> action_traces;
    }

	public class DetailedTransaction
    {
		
		public TransactionReceipt receipt;
		
		public Transaction trx;
    }

	public class PackedTransaction
    {
		
		public string compression;
		
		public List<object> context_free_data;
		
		public string id;
		
		public string packed_context_free_data;
		
		public string packed_trx;
		
		public List<string> signatures;
		
		public Transaction transaction;
    }

	public class RefundRequest
    {
		
		public string cpu_amount;
		
		public string net_amount;
    }

	public class SelfDelegatedBandwidth
    {
		
		public string cpu_weight;
		
		public string from;
		
		public string net_weight;
		
		public string to;
    }

	public class TotalResources
    {
		
		public string cpu_weight;
		
		public string net_weight;
		
		public string owner;
		
		public UInt64? ram_bytes;
    }

	public class VoterInfo
    {
		
		public bool? is_proxy;
		
		public double? last_vote_weight;
		
		public string owner;
		
		public List<string> producers;
		
		public double? proxied_vote_weight;
		
		public string proxy;
		
		public UInt64? staked;
    }

	public class ExtendedAsset
    {
		
		public string quantity;
		
		public string contract;
    }

	public class TableByScopeResultRow
    {
		
		public string code;
		
		public string scope;
		
		public string table;
		
		public string payer;
		
		public UInt32? count;
    }

	public class BlockHeader
    {
		
		public DateTime timestamp;
		
		public string producer;
		
		public UInt32 confirmed;
		
		public string previous;
		
		public string transaction_mroot;
		
		public string action_mroot;
		
		public UInt32 schedule_version;
		
		public object new_producers;
		
		public object header_extensions;
    }

	public class SignedBlockHeader
    {
		
		public DateTime timestamp;
		
		public string producer;
		
		public UInt32 confirmed;
		
		public string previous;
		
		public string transaction_mroot;
		
		public string action_mroot;
		
		public UInt32 schedule_version;
		
		public object new_producers;
		
		public object header_extensions;
		
		public string producer_signature;
    }

	public class Merkle
    {
		
		public List<string> _active_nodes;
		
		public UInt32 _node_count;
    }

	public class ActivedProtocolFeatures
    {
		
		public List<string> protocol_features;
    }
	#endregion

	#region generate api method types

    public class GetInfoResponse
    {
 
		public string server_version;
 
		public string chain_id;
 
		public UInt32 head_block_num;
 
		public UInt32 last_irreversible_block_num;
 
		public string last_irreversible_block_id;
 
		public string head_block_id;
 
		public DateTime head_block_time;
 
		public string head_block_producer;
 
		public string virtual_block_cpu_limit;
 
		public string virtual_block_net_limit;
 
		public string block_cpu_limit;
 
		public string block_net_limit;
    }

    public class GetAccountRequest
    {
		public string account_name;
    }

    public class GetAccountResponse
    {
 
		public string account_name;
 
		public UInt32 head_block_num;
 
		public DateTime head_block_time;
 
		public bool privileged;
 
		public DateTime last_code_update;
 
		public DateTime created;
 
		public Int64 ram_quota;
 
		public Int64 net_weight;
 
		public Int64 cpu_weight;
 
		public Resource net_limit;
 
		public Resource cpu_limit;
 
		public UInt64 ram_usage;
 
		public List<Permission> permissions;
 
		public RefundRequest refund_request;
 
		public SelfDelegatedBandwidth self_delegated_bandwidth;
 
		public TotalResources total_resources;
 
		public VoterInfo voter_info;
    }

    public class GetCodeRequest
    {
		public string account_name;
		public bool code_as_wasm;
    }

    public class GetCodeResponse
    {
 
		public string account_name;
 
		public string wast;
 
		public string wasm;
 
		public string code_hash;
 
		public Abi abi;
    }

    public class GetAbiRequest
    {
		public string account_name;
    }

    public class GetAbiResponse
    {
 
		public string account_name;
 
		public Abi abi;
    }

    public class GetRawCodeAndAbiRequest
    {
		public string account_name;
    }

    public class GetRawCodeAndAbiResponse
    {
 
		public string account_name;
 
		public string wasm;
 
		public string abi;
    }

    public class GetRawAbiRequest
    {
		public string account_name;
		public string abi_hash;
    }

    public class GetRawAbiResponse
    {
 
		public string account_name;
 
		public string code_hash;
 
		public string abi_hash;
 
		public string abi;
    }

    public class AbiJsonToBinRequest
    {
		public string code;
		public string action;
		public object args;
    }

    public class AbiJsonToBinResponse
    {
 
		public string binargs;
    }

    public class AbiBinToJsonRequest
    {
		public string code;
		public string action;
		public string binargs;
    }

    public class AbiBinToJsonResponse
    {
 
		public object args;
    }

    public class GetRequiredKeysRequest
    {
		public Transaction transaction;
		public List<string> available_keys;
    }

    public class GetRequiredKeysResponse
    {
 
		public List<string> required_keys;
    }

    public class GetBlockRequest
    {
		public string block_num_or_id;
    }

    public class GetBlockResponse
    {
 
		public DateTime timestamp;
 
		public string producer;
 
		public UInt32 confirmed;
 
		public string previous;
 
		public string transaction_mroot;
 
		public string action_mroot;
 
		public UInt32 schedule_version;
 
		public Schedule new_producers;
 
		public List<Extension> block_extensions;
 
		public List<Extension> header_extensions;
 
		public string producer_signature;
 
		public List<TransactionReceipt> transactions;
 
		public string id;
 
		public UInt32 block_num;
 
		public UInt32 ref_block_prefix;
    }

    public class GetBlockHeaderStateRequest
    {
		public string block_num_or_id;
    }

    public class GetBlockHeaderStateResponse
    {
 
		public Schedule active_schedule;
 
		public UInt32 bft_irreversible_blocknum;
 
		public UInt32 block_num;
 
		public string block_signing_key;
 
		public Merkle blockroot_merkle;
 
		public List<UInt32> confirm_count;
 
		public object confirmations;
 
		public UInt32 dpos_irreversible_blocknum;
 
		public UInt32 dpos_proposed_irreversible_blocknum;
 
		public SignedBlockHeader header;
 
		public string id;
 
		public Schedule pending_schedule;
 
		public ActivedProtocolFeatures activated_protocol_features;
 
		public List<List<string>> producer_to_last_produced;
 
		public List<List<string>> producer_to_last_implied_irb;
    }

    public class GetTableRowsRequest
    {
		public bool json = false;
		public string code;
		public string scope;
		public string table;
		public string table_key;
		public string lower_bound = "0";
		public string upper_bound = "-1";
		public Int32 limit = 10;
		public string key_type;
		public string index_position;
		public string encode_type = "dec";
		public bool reverse;
		public bool show_payer;
    }

    public class GetTableRowsResponse
    {
 
		public List<object> rows;
 
		public bool more;
    }

    public class GetTableRowsResponse<TRowType>
    {
   
		public List<TRowType> rows;
   
		public bool more;
    }

    public class GetTableByScopeRequest
    {
		public string code;
		public string table;
		public string lower_bound;
		public string upper_bound;
		public Int32 limit = 10;
		public bool reverse;
    }

    public class GetTableByScopeResponse
    {
 
		public List<TableByScopeResultRow> rows;
 
		public string more;
    }

    public class GetCurrencyBalanceRequest
    {
		public string code;
		public string account;
		public string symbol;
    }

    public class GetCurrencyBalanceResponse
    {
 
		public List<string> assets;
    }

    public class GetCurrencyStatsRequest
    {
		public string code;
		public string symbol;
    }

    public class GetCurrencyStatsResponse
    {
 
		public Dictionary<string, CurrencyStat> stats;
    }

    public class GetProducersRequest
    {
		public bool json = false;
		public string lower_bound;
		public Int32 limit = 50;
    }

    public class GetProducersResponse
    {
 
		public List<object> rows;
 
		public double total_producer_vote_weight;
 
		public string more;
    }

    public class GetProducerScheduleResponse
    {
 
		public Schedule active;
 
		public Schedule pending;
 
		public Schedule proposed;
    }

    public class GetScheduledTransactionsRequest
    {
		public bool json = false;
		public string lower_bound;
		public Int32 limit = 50;
    }

    public class GetScheduledTransactionsResponse
    {
 
		public List<ScheduledTransaction> transactions;
 
		public string more;
    }

    public class PushTransactionRequest
    {
		public string[] signatures;
		public UInt32 compression;
		public string packed_context_free_data;
		public string packed_trx;
		public Transaction transaction;
    }

    public class PushTransactionResponse
    {
 
		public string transaction_id;
 
		public ProcessedTransaction processed;
    }

    public class GetActionsRequest
    {
		public string account_name;
		public Int32 pos;
		public Int32 offset;
    }

    public class GetActionsResponse
    {
 
		public List<GlobalAction> actions;
 
		public UInt32 last_irreversible_block;
 
		public bool time_limit_exceeded_error;
    }

    public class GetTransactionRequest
    {
		public string id;
		public UInt32? block_num_hint;
    }

    public class GetTransactionResponse
    {
 
		public string id;
 
		public DetailedTransaction trx;
 
		public DateTime block_time;
 
		public UInt32 block_num;
 
		public UInt32 last_irreversible_block;
 
		public List<ActionTrace> traces;
    }

    public class GetKeyAccountsRequest
    {
		public string public_key;
    }

    public class GetKeyAccountsResponse
    {
 
		public List<string> account_names;
    }

    public class GetControlledAccountsRequest
    {
		public string controlling_account;
    }

    public class GetControlledAccountsResponse
    {
 
		public List<string> controlled_accounts;
    }
	#endregion
}

