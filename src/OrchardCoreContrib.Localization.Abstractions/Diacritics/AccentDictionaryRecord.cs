namespace OrchardCoreContrib.Localization.Diacritics
{
    public readonly struct AccentDictionaryRecord
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
    }
}
