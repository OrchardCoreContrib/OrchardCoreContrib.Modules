using System;

namespace OrchardCoreContrib.Localization.Diacritics
{
    public readonly struct AccentDictionaryRecord : IEquatable<AccentDictionaryRecord>
    {
        public AccentDictionaryRecord(char key, string value)
        {
            Key = key;
            Value = value;
        }

        public char Key { get; }

        public string Value { get; }

        public static bool operator == (AccentDictionaryRecord l, AccentDictionaryRecord r)
            => l.Key == r.Key && l.Value == r.Value;

        public static bool operator != (AccentDictionaryRecord l, AccentDictionaryRecord r)
            => !(l == r);

        public override int GetHashCode() => HashCode.Combine(Key, Value);

        public override bool Equals(object obj)
        {
            if (obj is AccentDictionaryRecord other)
            {
                return Equals(other);
            }

            return false;
        }

        public bool Equals(AccentDictionaryRecord other)
            => Char.Equals(Key, other.Key) && String.Equals(Value, other.Value);

        public override string ToString() => $"{Key},{Value}";
    }
}
