using OrchardCore.Localization;
using OrchardCoreContrib.Infrastructure;
using System.Text.Json;

namespace OrchardCoreContrib.Localization.Json;

/// <summary>
/// Reprensents a JSON reader.
/// </summary>
public class JsonReader
{
    /// <summary>
    /// Parses a given JSON file.
    /// </summary>
    /// <param name="stream">The <see cref="Stream"/> to be parsed.</param>
    public async Task<IEnumerable<CultureDictionaryRecord>> ParseAsync(Stream stream)
    {
        Guard.ArgumentNotNull(stream, nameof(stream));

        var document = await JsonDocument.ParseAsync(stream);
        var cultureRecords = new List<CultureDictionaryRecord>();
        foreach (var item in document.RootElement.EnumerateObject())
        {
            cultureRecords.Add(new CultureDictionaryRecordWrapper(item.Name, item.Value.ToString()));
        }

        return cultureRecords;
    }
}
