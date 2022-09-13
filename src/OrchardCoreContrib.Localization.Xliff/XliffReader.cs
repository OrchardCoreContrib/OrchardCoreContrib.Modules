using OrchardCore.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace OrchardCoreContrib.Localization.Xliff;

public class XliffReader
{
    public static async Task<IEnumerable<CultureDictionaryRecord>> ParseAsync(Stream stream)
    {
        if (stream is null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        try
        {
            var doc = await XDocument.LoadAsync(stream, LoadOptions.None, CancellationToken.None);

            var cultureRecords = new List<CultureDictionaryRecord>();
            var @namespace = doc.Root.Name.Namespace;
            foreach (var unitElement in doc.Descendants(@namespace + "unit"))
            {
                var segmentElement = unitElement.Element(@namespace + "segment");
                var key = segmentElement.Element(@namespace + "source").Value;
                var value = segmentElement.Element(@namespace + "target").Value;

                cultureRecords.Add(new CultureDictionaryRecordWrapper(key, value));
            }

            return cultureRecords;
        }
        catch (Exception ex)
        {
            throw new XliffException("Invalid XLIFF document.", ex);
        }
    }
}
