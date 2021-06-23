using System;
using System.Threading;
using Newtonsoft.Json;
using System.Threading.Tasks;
using GraphEosStreamer.SHiP.Tables;


namespace GraphEosStreamer.SHiP.Variants
{
    public abstract class TableDelta
    {
        public abstract string ToJson();
    }

    [Serializable()]
    public class TableDeltaV0 : TableDelta
    {
        // abi-field-name: name ,abi-field-type: string
        [JsonProperty("name")]
        public string Name;

        // abi-field-name: rows ,abi-field-type: row[]
        [JsonProperty("rows")]
        public Row[] Rows;

        public TableDeltaV0(string name, Row[] rows)
        {
            this.Name = name;
            this.Rows = rows;
        }

        public TableDeltaV0()
        {
        }

        public async Task DeserializeAsync(CancellationToken ctl)
        {
            await Task.Run(Deserialize, ctl);
        }

        public void Deserialize()
        {
            foreach (var row in Rows)
            {
                row.Table = (Table)Deserializer.Deserializer.ReadRowData(row.Data, Name);
            }
        }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}