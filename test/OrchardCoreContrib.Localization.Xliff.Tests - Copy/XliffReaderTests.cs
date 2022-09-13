using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OrchardCoreContrib.Localization.Xliff.Tests;

public class XliffReaderTests
{
    [Fact]
    public async Task XliffReader_Parse_ReturnsCultureDictionaryRecords()
    {
        // Arrange
        var stream = GetXliffStream();

        // Act
        var records = await XliffReader.ParseAsync(stream);

        // Assert
        Assert.NotNull(records);
        Assert.Equal(2, records.Count());
    }

    [Fact]
    public async Task XliffReader_ParseEmptyOrInvalidJson_ThrowsXliffException()
    {
        // Arrange
        var stream = new MemoryStream();

        // Act & Assert
        await Assert.ThrowsAsync<XliffException>(async () => await XliffReader.ParseAsync(stream));
    }

    [Fact]
    public async Task XliffReader_Parse_ShouldNotReturnRawText()
    {
        // Arrange
        var stream = GetXliffStream();

        // Act
        var records = await XliffReader.ParseAsync(stream);

        // Assert
        Assert.NotNull(records);
        Assert.NotEqual("\"Bonjour\"", records.First().Translations[0]);
    }

    private static Stream GetXliffStream()
    {
        var json = @"<xliff xmlns=""urn:oasis:names:tc:xliff:document:2.0"" version=""2.0""
 srcLang=""en-US"" trgLang=""fr-FR"">
 <file id=""f1"" original=""Graphic Example.psd"">
  <skeleton href=""Graphic Example.psd.skl""/>
  <unit id=""1"">
   <segment>
    <source>Hello</source>
    <target>Bonjour</target>
   </segment>
  </unit>
  <unit id=""2"">
   <segment>
    <source>Bye</source>
    <target>au revoir</target>
   </segment>
  </unit>
 </file>
</xliff>";
        var memoryStream = new MemoryStream();
        var streamWriter = new StreamWriter(memoryStream);

        streamWriter.WriteLine(json);
        streamWriter.Flush();
        memoryStream.Seek(0, SeekOrigin.Begin);

        return memoryStream;
    }
}
