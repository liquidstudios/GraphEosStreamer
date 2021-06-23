using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using GraphEosStreamer.Other;
using GraphEosStreamer.SHiP.Variants;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using Newtonsoft.Json;

namespace GraphEosStreamer.SHiP
{

    public class Bytes
    {
        [JsonIgnore]
        internal byte[] _value;

        public static implicit operator Bytes(byte[] value)
        {
            return new() { _value = value };
        }

        public static implicit operator byte[](Bytes value)
        {
            return value._value;
        }

        public string ToJson()
        {
            return SerializationHelper.ByteArrayToHexString(_value);
        }
    }

    public class Bytes<T> : Bytes
    {
        public T Instance;

        [JsonIgnore]
        public bool IsDeserialized => Instance != null;

        public T GetInstance()
        {
            return Instance;
        }

        public async Task DeserializeAsync(CancellationToken cancellationToken)
        {
            if (IsDeserialized)
                return;

            Instance = await Deserializer.Deserializer.DeserializeAsync<T>(_value, cancellationToken);
        }

        public void Deserialize()
        {
            if (IsDeserialized)
                return;

            Instance = Deserializer.Deserializer.Deserialize<T>(_value);
        }

        public static implicit operator Bytes<T>(byte[] value)
        {
            return new() { _value = value };
        }

        public static implicit operator byte[](Bytes<T> value)
        {
            return value._value;
        }
    }

    public class ContractRowBytes : Bytes<object>
    {
        public ContractRowBytes(byte[] bytes)
        {
            this._value = bytes;
        }

        public static implicit operator ContractRowBytes(byte[] value)
        {
            return new(value);;
        }

        public static implicit operator byte[](ContractRowBytes value)
        {
            return value._value;
        }
    }

    public class ActionBytes : Bytes<object>
    {
        public ActionBytes(Bytes bytes)
        {
            this._value = bytes._value;
        }

        public static implicit operator ActionBytes(byte[] value)
        {
            return new(value); ;
        }

        public static implicit operator byte[](ActionBytes value)
        {
            return value._value;
        }
    }

    public class TracesBytes : Bytes<TransactionTrace[]>
    {
        public TracesBytes(Bytes bytes)
        {
            this._value = bytes._value;
        }
    }

    public class DeltasBytes : Bytes<TableDelta[]>
    {
        public DeltasBytes(Bytes bytes)
        {
            this._value = bytes._value;
        }
    }

    public class BlockBytes : Bytes<SignedBlock>
    {
        public BlockBytes(Bytes bytes)
        {
            this._value = bytes._value;
        }
    }

    public class PackedTransactionBytes : Bytes<Transaction>
    {
        public PackedTransactionBytes(Bytes bytes)
        {
            this._value = bytes._value;
        }

        public void DeserializeCompressed(byte compression)
        {
            if (IsDeserialized)
                return;
            if (compression == (int)Compression.Zlib)
            {
                using (var unpackedStream = new MemoryStream())
                {
                    using (var packedStream = new InflaterInputStream(new MemoryStream(_value)))
                    {
                        packedStream.CopyTo(unpackedStream);
                        Instance = Deserializer.Deserializer.Deserialize<Transaction>(unpackedStream.ToArray());
                    }
                }
            }
        }

        public async Task DeserializeCompressedAsync(byte compression, CancellationToken cancellationToken)
        {
            if (IsDeserialized)
                return;
            if (compression == (int) Compression.Zlib)
            {
                using (var unpackedStream = new MemoryStream())
                {
                    using (var packedStream = new InflaterInputStream(new MemoryStream(_value)))
                    {
                        await packedStream.CopyToAsync(unpackedStream, cancellationToken);
                        Instance = await Deserializer.Deserializer.DeserializeAsync<Transaction>(unpackedStream.ToArray(), cancellationToken);
                    }
                }
            }
        }

        public new string ToJson()
        {
            return SerializationHelper.ByteArrayToHexString(_value);
        }
    }

    public class BytesConverter : JsonConverter<Bytes>
    {
        public override void WriteJson(JsonWriter writer, Bytes value, JsonSerializer serializer)
        {
            if (value is PackedTransactionBytes packed && packed.Instance != null)
            {
                writer.WriteValue(JsonConvert.SerializeObject(packed.Instance));
            }
            else
            {
                writer.WriteValue(SerializationHelper.ByteArrayToHexString(value));
            }
        }

        public override Bytes ReadJson(JsonReader reader, Type objectType, Bytes existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return (Bytes)SerializationHelper.Base64FcStringToByteArray((string)reader.Value);
        }
    }
}
