using System.Collections.Generic;
using Xunit;

namespace OrchardCoreContrib.Localization.Diacritics.Tests;

public class DiacriticsLookupExtensionsTests
{
    [Fact]
    public void BuildDiacriticsLookup()
    {
        // Arrange
        var lookup = new DiacriticsLookup(new List<IAccentMapper>
            {
                new ArabicAccentMapper(),
                new UkraniaAccentMapper()
            });

        // Act
        var entries = lookup.Get("ar");

        // Aseert
        Assert.NotEmpty(entries);
        Assert.Equal(7, entries.Count);
    }
}
