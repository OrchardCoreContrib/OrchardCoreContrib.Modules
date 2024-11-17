namespace OrchardCoreContrib.Localization.Diacritics;

/// <summary>
/// Represents a record within <see cref="AccentDictionary"/>.
/// </summary>
/// <remarks>
/// Initializes a new instance of a <see cref="AccentDictionaryRecord"/>.
/// </remarks>
/// <param name="key">The key to be associates with the record.</param>
/// <param name="value">The value to be associates with the record.</param>
public readonly struct AccentDictionaryRecord(char key, string value) : IEquatable<AccentDictionaryRecord>
{

    /// <summary>
    /// Gets the key.
    /// </summary>
    public char Key { get; } = key;

    /// <summary>
    /// Gets the value.
    /// </summary>
    public string Value { get; } = value;

    public static bool operator ==(AccentDictionaryRecord l, AccentDictionaryRecord r)
        => l.Key == r.Key && l.Value == r.Value;

    public static bool operator !=(AccentDictionaryRecord l, AccentDictionaryRecord r)
        => !(l == r);

    /// <inheritdoc/>
    public override int GetHashCode() => HashCode.Combine(Key, Value);

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj is AccentDictionaryRecord other)
        {
            return Equals(other);
        }

        return false;
    }

    /// <inheritdoc/>
    public bool Equals(AccentDictionaryRecord other)
        => Char.Equals(Key, other.Key) && String.Equals(Value, other.Value);

    /// <inheritdoc/>
    public override string ToString() => $"{Key},{Value}";
}
