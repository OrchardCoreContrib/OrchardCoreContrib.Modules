using System;

namespace OrchardCoreContrib.Localization.Diacritics;

/// <summary>
/// Represents a record within <see cref="AccentDictionary"/>.
/// </summary>
public readonly struct AccentDictionaryRecord : IEquatable<AccentDictionaryRecord>
{
    /// <summary>
    /// Initializes a new instance of a <see cref="AccentDictionaryRecord"/>.
    /// </summary>
    /// <param name="key">The key to be associates with the record.</param>
    /// <param name="value">The value to be associates with the record.</param>
    public AccentDictionaryRecord(char key, string value)
    {
        Key = key;
        Value = value;
    }

    /// <summary>
    /// Gets the key.
    /// </summary>
    public char Key { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    public string Value { get; }

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
