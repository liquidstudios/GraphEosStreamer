using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using GraphEosStreamer.AssemblyGenerator;
using GraphEosStreamer.GraphQl.Schemas.Block;
using GraphEosStreamer.SHiP.Tables;
using GraphEosStreamer.SHiP.Variants;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

namespace GraphEosStreamer.BlockReader
{
    public class DeltaDeserializerService : BackgroundService
    {
        private readonly ChannelReader<GetBlocksResultV0> _blocksResultChannel;
        private readonly ChannelWriter<DeserializedGetBlocksResultV0> _deserializedBlocksResultChannel;

        private readonly IBlockStream _blockStream;

        private readonly Options _options;

        private readonly SortedSet<string> _ignoreContracts;

        private readonly int _deltaDeserializerTasks;
        private bool _mergeDeltas;

        public DeltaDeserializerService(ChannelReader<GetBlocksResultV0> blocksResultChannel,
            ChannelWriter<DeserializedGetBlocksResultV0> deserializedBlocksResultChannel, IBlockStream blockStream,
            IOptionsMonitor<Options> optionsMonitor)
        {
            _options = optionsMonitor.CurrentValue;
            _ignoreContracts = _options.IgnoreContracts ?? new SortedSet<string>();
            _mergeDeltas = _options.MergeDeltas;

            _blocksResultChannel = blocksResultChannel;
            _deserializedBlocksResultChannel = deserializedBlocksResultChannel;
            _blockStream = blockStream;
            _deltaDeserializerTasks = _options.DeltaDeserializerTasks;

            optionsMonitor.OnChange(OnOptionsChanged);
        }

        private void OnOptionsChanged(Options options)
        {
            _mergeDeltas = options.MergeDeltas;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var deserializerTasks = new List<Task>();
            for (var i = 0; i < _deltaDeserializerTasks; i++)
            {
                deserializerTasks.Add(DeserializeDetails(i, cancellationToken));
            }

            await Task.WhenAll(deserializerTasks);
        }


        private async Task DeserializeDetails(int taskNum, CancellationToken clt)
        {
            await foreach (var getBlocksResultV0 in _blocksResultChannel.ReadAllAsync(clt))
            { 
                try
                {
                    if(getBlocksResultV0.ThisBlock == null)
                        continue;

                    // Deserialize Deltas, filter AccountV0-Rows (abi-updates)
                    if (getBlocksResultV0.DeltasBytes?.Instance != null)
                        Parallel.ForEach(getBlocksResultV0.DeltasBytes.Instance, (tableDelta) =>
                        {
                            if (tableDelta is TableDeltaV0 {Rows: {Length: > 0}} tableDeltaV0)
                            {
                                tableDeltaV0.Deserialize();

                                foreach (var row in tableDeltaV0.Rows)
                                {
                                    if (row.Table is AccountV0 {Abi: {_value: {Length: > 0}}} accountV0)
                                        try
                                        {
                                            RuntimeAssemblyGenerator.CreateAbiAndAssembly(accountV0.Name,
                                                accountV0.Abi._value, getBlocksResultV0.ThisBlock.BlockNum);
                                        }
                                        catch (Exception e)
                                        {
                                            Log.Error(e, "");
                                            Log.Error("Contract: " + accountV0.Name + " at Block: " + getBlocksResultV0.ThisBlock.BlockNum);
                                        }
                                }
                            }
                        });

                    if (getBlocksResultV0.DeltasBytes?.Instance != null)
                        Parallel.ForEach(getBlocksResultV0.DeltasBytes.Instance, (tableDelta) =>
                        {
                            if (tableDelta is TableDeltaV0 { Rows: { Length: > 0 } } tableDeltaV0)
                            {
                                tableDeltaV0.Deserialize();

                                foreach (var row in tableDeltaV0.Rows)
                                {
                                    if (row.Table is ContractRowV0 contractRowV0)
                                    {
                                        if (!_ignoreContracts.Contains(contractRowV0.Code) && AssemblyCache.ContractAssemblyCache.TryGetValue(contractRowV0.Code,
                                                out var contractTypes) &&
                                            contractTypes[contractTypes.Keys.Max()].TryGetTableType(contractRowV0.Table, out var tableType))
                                        {
                                            try
                                            {
                                                contractRowV0.Value.Instance =
                                                    Deserializer.Deserializer.Deserialize(contractRowV0.Value._value,
                                                        tableType);
                                            }
                                            catch (Exception e)
                                            {
                                                Log.Error(e,"");
                                                if(AssemblyCache.ContractAssemblyCache.TryRemove(contractRowV0.Code, out contractTypes))
                                                    Log.Information(contractRowV0.Code + " removed from AssemblyCache");
                                            }
                                        }
                                    }
                                }
                            }
                        });


                    if (getBlocksResultV0.BlockBytes?.Instance?.Transactions != null)
                        Parallel.ForEach(getBlocksResultV0.BlockBytes?.Instance?.Transactions, (transaction) =>
                        {
                            if (transaction.Trx is PackedTransactionVariant packedTrx)
                            {
                                if (packedTrx.Compression != (int) Compression.None)
                                    packedTrx.PackedTrx.DeserializeCompressed(packedTrx.Compression);
                                else
                                    packedTrx.PackedTrx.Deserialize();

                                foreach (var action in packedTrx.PackedTrx.Instance.Actions)
                                {
                                    if (!_ignoreContracts.Contains(action.Account) && AssemblyCache.ContractAssemblyCache.TryGetValue(action.Account,
                                            out var contractTypes) &&
                                        contractTypes[contractTypes.Keys.Max()]
                                            .TryGetActionType(action.Name, out var actionTypes))
                                    {
                                        try
                                        {
                                            action.Data.Instance =
                                                Deserializer.Deserializer.Deserialize(action.Data._value, actionTypes);
                                        }
                                        catch (Exception e)
                                        {
                                            Log.Error(e, "");
                                            if(AssemblyCache.ContractAssemblyCache.TryRemove(action.Account, out contractTypes))
                                                Log.Information(action.Account + " removed from AssemblyCache");
                                        }
                                    }
                                }
                            }
                        });

                    if(getBlocksResultV0.ThisBlock?.BlockNum % 50000 == 0)
                        Log.Information("task" + taskNum + " fully deserialized Block " + getBlocksResultV0.ThisBlock?.BlockNum + " at " + DateTime.Now.ToLongTimeString() + "            Traces:" + getBlocksResultV0.TracesBytes?.Instance?.Length + " ,Deltas:" + getBlocksResultV0.DeltasBytes?.Instance?.Length);

                    if(_mergeDeltas)
                        _blockStream.AddBlock(new Block(getBlocksResultV0));

                    await _deserializedBlocksResultChannel.WriteAsync(new DeserializedGetBlocksResultV0(getBlocksResultV0), clt);
                }
                catch (Exception e)
                {
                    Log.Error(e,"");
                }
            }
        }

        private void HandleAccountDelta(Account account)
        {
            switch (account)
            {
                case AccountV0 accountV0:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleAccountMetadataDelta(AccountMetadata accountMetadata)
        {
            switch (accountMetadata)
            {
                case AccountMetadataV0 accountMetadataV0:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleCodeDelta(Code code)
        {
            switch (code)
            {
                case CodeV0 codeV0:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleContractIndexDoubleDelta(ContractIndexDouble contractIndexDouble)
        {
            switch (contractIndexDouble)
            {
                case ContractIndexDoubleV0 contractIndexDoubleV0:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleContractRowDelta(ContractRow contractRow)
        {
            switch (contractRow)
            {
                case ContractRowV0 contractRowV0:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleContractTableDelta(ContractTable contractTable)
        {
            switch (contractTable)
            {
                case ContractTableV0 contractTableV0:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleGlobalPropertyDelta(GlobalProperty globalProperty)
        {
            switch (globalProperty)
            {
                case GlobalPropertyV0 globalPropertyV0:
                    break;
                case GlobalPropertyV1 globalPropertyV1:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandlePermissionDelta(Permission permission)
        {
            switch (permission)
            {
                case PermissionV0 permissionV0:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleResourceLimitsDelta(ResourceLimits resourceLimits)
        {
            switch (resourceLimits)
            {
                case ResourceLimitsV0 resourceLimitsV0:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleResourceLimitsConfigDelta(ResourceLimitsConfig resourceLimitsConfig)
        {
            switch (resourceLimitsConfig)
            {
                case ResourceLimitsConfigV0 resourceLimitsConfigV0:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleResourceLimitsStateDelta(ResourceLimitsState resourceLimitsState)
        {
            switch (resourceLimitsState)
            {
                case ResourceLimitsStateV0 resourceLimitsStateV0:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleResourceUsageDelta(ResourceUsage resourceUsage)
        {
            switch (resourceUsage)
            {
                case ResourceUsageV0 resourceUsageV0:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        /*                          switch (tableDeltaV0.TableDeltaV0Type)
                            {


                                case Account<IAccount> account:
                                    HandleAccountDelta(account);
                                    break;
                                case AccountMetadata<IAccountMetadata> accountMetadata:
                                    HandleAccountMetadataDelta(accountMetadata);
                                    break;
                                case Code<ICode> code:
                                    HandleCodeDelta(code);
                                    break;
                                case ContractIndexDouble<IContractIndexDouble> contractIndexDouble:
                                    HandleContractIndexDoubleDelta(contractIndexDouble);
                                    break;
                                case ContractRow<IContractRow> contractRow:
                                    HandleContractRowDelta(contractRow);
                                    break;
                                case ContractTable<IContractTable> contractTable:
                                    HandleContractTableDelta(contractTable);
                                    break;
                                case GlobalProperty<IGlobalProperty> globalProperty:
                                    HandleGlobalPropertyDelta(globalProperty);
                                    break;
                                case Permission<IPermission> permission:
                                    HandlePermissionDelta(permission);
                                    break;
                                case ResourceLimits<IResourceLimits> resourceLimits:
                                    HandleResourceLimitsDelta(resourceLimits);
                                    break;
                                case ResourceLimitsConfig<IResourceLimitsConfig> resourceLimitsConfig:
                                    HandleResourceLimitsConfigDelta(resourceLimitsConfig);
                                    break;
                                case ResourceLimitsState<IResourceLimitsState> resourceLimitsState:
                                    HandleResourceLimitsStateDelta(resourceLimitsState);
                                    break;
                                case ResourceUsage<IResourceUsage> resourceUsage:
                                    HandleResourceUsageDelta(resourceUsage);
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
}*/
    }
}
