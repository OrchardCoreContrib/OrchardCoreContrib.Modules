using OrchardCoreContrib.ContentLocalization.Services;
using OrchardCoreContrib.ContentLocalization.Transliteration;
using Xunit;

namespace OrchardCoreContrib.Modules.ContentLocalization.Tests;

public class TransliterationServiceTests
{
    [Theory]
    [InlineData(TransliterateScript.Arabic, "ضروري", "drwry")]
    [InlineData(TransliterateScript.Arabic, "الوثائق والشهادات", "alwtha'eq walshhadat")]
    [InlineData(TransliterateScript.Cyrillic, "НЕОПХОДНИ", "NEOPHODNI")]
    [InlineData(TransliterateScript.Cyrillic, "Документа и уверења", "Dokumenta i uvereњa")]
    public void TransliterateText(TransliterateScript script, string text, string expectedTransliteratedText)
    {
        // Arrange
        var transliterationService = new TransliterationService(new DefaultTransliterateRuleProvider());

        // Act
        var transliteratedText = transliterationService.Transliterate(script, text);

        // Assert
        Assert.Equal(expectedTransliteratedText, transliteratedText);
    }
}
