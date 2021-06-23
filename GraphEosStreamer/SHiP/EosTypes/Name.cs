namespace GraphEosStreamer.SHiP.EosTypes
{
    public class Name
    {
        private readonly ulong _binary;

        private readonly string _value;

        /*
        public static implicit operator Name(string value, ulong )
        {
            return new() { _value = value };
        }
        */

        public static implicit operator string(Name value)
        {
            return value._value;
        }

        public static implicit operator ulong(Name value)
        {
            return value._binary;
        }

        public string ToJson()
        {
            return _value;
        }

        public Name(ulong binary, string value)
        {
            _value = value;
            _binary = binary;
        }


        public override bool Equals(object obj)
        {
            if (obj is not Name item)
            {
                return false;
            }

            return this._binary.Equals(item._binary) || _value.Equals(item._value);
        }

        public override int GetHashCode()
        {
            return this._binary.GetHashCode();
        }
    }
}