namespace OrchardCoreContrib.ContentLocalization.Transliteration.Tests;

public class DefaultTransliterationRuleProviderTests
{
    [Fact]
    public void ValidateScriptLetters()
    {
        // Arrange
        var transliterateRuleProvider = new DefaultTransliterateRuleProvider();

        // Act
        transliterateRuleProvider.TryGetRule(TransliterateScript.Arabic, out var rule);

        // Assert
        var arabicAlphabet = rule().Keys;

        Assert.Equal(33, arabicAlphabet.Count);
        Assert.True(arabicAlphabet.Contains("ه"));
        Assert.True(arabicAlphabet.Contains("ش"));
        Assert.True(arabicAlphabet.Contains("ا"));
        Assert.True(arabicAlphabet.Contains("م"));
        Assert.False(arabicAlphabet.Contains("x"));
    }

    [Theory]
    [InlineData(TransliterateScript.Arabic, "ح", "h")]
    [InlineData(TransliterateScript.Arabic, "ث", "th")]
    [InlineData(TransliterateScript.Cyrillic, "л", "l")]
    [InlineData(TransliterateScript.Cyrillic, "Д", "D")]
    public void TransliterateValue(TransliterateScript script, string value, string expectedTransliteratedValue)
    {
        // Arrange
        var transliterateRuleProvider = new DefaultTransliterateRuleProvider();

        // Act
        transliterateRuleProvider.TryGetRule(script, out var rule);

        var mappings = rule.Invoke();

        // Assert
        Assert.Equal(expectedTransliteratedValue, mappings[value]);
    }
}
