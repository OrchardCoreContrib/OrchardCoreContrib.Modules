using OrchardCore.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Localization.Json
{
    public class JsonReader
    {
        public async Task<IEnumerable<CultureDictionaryRecord>> ParseAsync(Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            var document = await JsonDocument.ParseAsync(stream);
            var cultureRecords = new List<CultureDictionaryRecord>();
            foreach (var item in document.RootElement.EnumerateObject())
            {
                cultureRecords.Add(new CultureDictionaryRecordWrapper(item.Name, item.Value.ToString()));
            }

            return cultureRecords;
        }
    }
}
