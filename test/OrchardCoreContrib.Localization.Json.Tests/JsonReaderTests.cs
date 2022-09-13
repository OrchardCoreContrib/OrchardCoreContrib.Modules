using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace OrchardCoreContrib.Localization.Json.Tests;

public class JsonReaderTests
{
    [Fact]
    public async Task JsonReader_Parse_ReturnsCultureDictionaryRecords()
    {
        // Arrange
        var reader = new JsonReader();
        var stream = GetJsonStream();

        // Act
        var records = await reader.ParseAsync(stream);

        // Assert
        Assert.NotNull(records);
        Assert.Equal(4, records.Count());
    }

    [Fact]
    public async Task JsonReader_ParseEmptyOrInvalidJson_ThrowsJsonReaderException()
    {
        // Arrange
        var reader = new JsonReader();
        var stream = new MemoryStream();

        // Act & Assert
        var exception = await Assert.ThrowsAnyAsync<JsonException>(async () => await reader.ParseAsync(stream));
        // HACK: workaround to check if the exception is JsonReaderException, because it's internal
        Assert.IsAssignableFrom<JsonException>(exception);
        Assert.Equal("JsonReaderException", exception.GetType().Name);
    }

    [Fact]
    public async Task JsonReader_Parse_ShouldNotReturnRawText()
    {
        // Arrange
        var reader = new JsonReader();
        var stream = GetJsonStream();

        // Act
        var records = await reader.ParseAsync(stream);

        // Assert
        Assert.NotNull(records);
        Assert.NotEqual("\"Bonjour\"", records.First().Translations[0]);
    }

    private static Stream GetJsonStream()
    {
        var json = @"{
    ""Hello"": ""Bonjour"",
    ""Bye"": ""au revoir"",
    ""Yes"": ""Oui"",
    ""No"": ""Non""
}";
        var memoryStream = new MemoryStream();
        var streamWriter = new StreamWriter(memoryStream);

        streamWriter.WriteLine(json);
        streamWriter.Flush();
        memoryStream.Seek(0, SeekOrigin.Begin);

        return memoryStream;
    }
}
