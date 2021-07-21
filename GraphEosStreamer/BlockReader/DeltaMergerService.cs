using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using GraphEosStreamer.AssemblyGenerator;
using GraphEosStreamer.GraphQl.Schemas.Block;
using GraphEosStreamer.SHiP;
using GraphEosStreamer.SHiP.Tables;
using GraphEosStreamer.SHiP.Variants;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Action = GraphEosStreamer.SHiP.Action;

namespace GraphEosStreamer.BlockReader
{
    public class DeltaMergerService : BackgroundService
    {
        private readonly ChannelReader<DeserializedGetBlocksResultV0> _deserializedBlocksResultChannel;

        private readonly Options _options;

        private readonly SortedSet<string> _ignoreContracts;

        private readonly int _deltaMergerTasks;
        private bool _streamBlocks;

        public DeltaMergerService(ChannelReader<DeserializedGetBlocksResultV0> deserializedBlocksResultChannel,
            IOptionsMonitor<Options> optionsMonitor)
        {
            _options = optionsMonitor.CurrentValue;
            _ignoreContracts = _options.IgnoreContracts ?? new SortedSet<string>();
            _streamBlocks = _options.StreamBlocks;

            _deserializedBlocksResultChannel = deserializedBlocksResultChannel;
            _deltaMergerTasks = _options.DeltaDeserializerTasks;

            optionsMonitor.OnChange(OnOptionsChanged);
        }

        private void OnOptionsChanged(Options options)
        {
            _streamBlocks = options.StreamBlocks;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var deserializerTasks = new List<Task>();
            for (var i = 0; i < _deltaMergerTasks; i++)
            {
                deserializerTasks.Add(MergeDetails(i, cancellationToken));
            }

            await Task.WhenAll(deserializerTasks);
        }


        private async Task MergeDetails(int taskNum, CancellationToken clt)
        {
            await foreach (var deserializedGetBlocksResultV0 in _deserializedBlocksResultChannel.ReadAllAsync(clt))
            { 
                try
                {
                    var getBlocksResultV0 = deserializedGetBlocksResultV0.GetBlocksResultV0;
                    if(getBlocksResultV0.DeltasBytes == null || getBlocksResultV0.DeltasBytes?.Instance?.Length == 0 || getBlocksResultV0.TracesBytes == null || getBlocksResultV0.TracesBytes?.Instance?.Length == 0)
                        continue;
/*                    foreach (var transaction in getBlocksResultV0.BlockBytes.Instance.Transactions)
                    {
                        if (transaction.Trx is PackedTransactionVariant packedTransaction)
                        {*/
                    // packedTransaction.PackedTrx.Instance.Actions;
                    //                          foreach (var action in packedTransaction.PackedTrx.Instance.Actions)
                    //                          {
                    /*                                action.Account;
                                                    action.Authorization;
                                                    action.Name;
                                                    action.Data.Instance*/
                    /* }
                 }*/

                    /*                        foreach (var transactionTrace in getBlocksResultV0.TracesBytes.Instance)
                                            {
                                                if (transactionTrace is TransactionTraceV0 transactionTraceV0)
                                                {
                    //                                 transactionTraceV0.AccountRamDelta
                                                    foreach (var actionTrace in transactionTraceV0.ActionTraces)
                                                    {
                                                        if (actionTrace is ActionTraceV0 actionTraceV0)
                                                        {
                    /*                                        actionTraceV0.AccountRamDeltas;
                                                            actionTraceV0.AccountRamDeltas[0].Account;
                                                            actionTraceV0.AccountRamDeltas[0].Delta; // long
                                                            actionTraceV0.Act;
                                                            actionTraceV0.Act.Account;
                                                            actionTraceV0.Act.Authorization[0].Actor;
                                                            actionTraceV0.Act.Name;
                                                            actionTraceV0.Receiver;
                                                            (actionTraceV0.Receipt as ActionReceiptV0).AuthSequence[0].Account;
                                                            (actionTraceV0.Receipt as ActionReceiptV0).Receiver;*/
                    /*         }
                         }
                     }
                 }*/

                        var removeList = new List<string>() {"account_metadata", "resource_usage", "resource_limits_state", "resource_limits_config", "resource_limits", "account", "permission" };
                        var i = 0;

                        var contractRows = getBlocksResultV0.DeltasBytes.Instance
                            .Where(d => d is TableDeltaV0 {Name: "contract_row"}).SelectMany(td => ((TableDeltaV0)td).Rows.Select(r => new Tuple<long,ContractRowV0>(r.Data._value.Length, (ContractRowV0)r.Table)))
                            .ToList();

                        List<Tuple<Action, ContractRowV0>> matches = new List<Tuple<Action, ContractRowV0>>();

                        if (contractRows is {Count: > 0})
                        {
                            foreach (var transactionTraceV0 in getBlocksResultV0.TracesBytes.Instance.Where(t => t is TransactionTraceV0).Cast<TransactionTraceV0>())
                            {
                                var accountRamDelta = transactionTraceV0.AccountRamDelta?.Delta ?? 0;
                                var ramDeltasSum = transactionTraceV0.ActionTraces?.Cast<ActionTraceV0>()
                                    .Sum(a => a.AccountRamDeltas?.Sum(ard => ard.Delta));

    //                            Console.WriteLine($"accountRamDelta:{accountRamDelta} ramDeltasSum:{ramDeltasSum ?? 0}");

                                var ia = 0;
                                foreach (var actionTraceV0 in transactionTraceV0.ActionTraces.Cast<ActionTraceV0>())
                                {
                                    if (actionTraceV0.AccountRamDeltas is {Length: > 0} && !((string)actionTraceV0.Act.Account).Contains("eosio") && actionTraceV0.Act.Name != "setcode" && actionTraceV0.Act.Name != "onblock")
                                    {
                                        long sum = 0;
                                        long deltaSum = actionTraceV0.AccountRamDeltas.Sum(ard => ard.Delta);
                                        while (sum != deltaSum)
                                        {
                                            var contractRow = contractRows.FirstOrDefault(cr => cr.Item2.Code == actionTraceV0.Act.Account);
                                            if (contractRow != null)
                                            {
                                                sum += contractRow.Item1;
                                                matches.Add(new Tuple<Action, ContractRowV0>(actionTraceV0.Act, contractRow.Item2));
                                                contractRows.Remove(contractRow);
                                            }
                                            else
                                            {
                                                Console.WriteLine("deltaSum:" + deltaSum + " sum:" + sum);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (matches.Count > 0)
                            Console.WriteLine($"matches: {matches.Count}");
/*                        var transactionTracesV0 = getBlocksResultV0.TracesBytes.Instance.Where(t => t is TransactionTraceV0).Cast<TransactionTraceV0>();
                        foreach (var tableDeltaV0 in getBlocksResultV0.DeltasBytes.Instance.Where(d => d is TableDeltaV0).Cast<TableDeltaV0>())
                        {
                            if (tableDeltaV0.Rows[0].Table is ContractRowV0 contractRowV0)
                            {
/*                                    contractRowV0.Value._value.Length;
                                contractRowV0.Code;
                                contractRowV0.PrimaryKey
                                contractRowV0.Payer;
                                contractRowV0.Table;
                                contractRowV0.Scope;
                                contractRowV0.Value.Instance*/
                       /*     }
                        }
                    }*/

                }
                catch (Exception e)
                {
                    Log.Error(e,"");
                }
            }
        }

    }
}
