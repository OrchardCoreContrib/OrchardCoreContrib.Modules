using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OrchardCoreContrib.Localization.Xliff.Tests;

public class XliffReaderTests
{
    private static readonly string _content_1_1 = @"<xliff version='1.1' xmlns='urn:oasis:names:tc:xliff:document:1.1'>
    <file original='hello.txt' source-language='en' target-language='fr' datatype='plaintext'>
        <body>
            <trans-unit id='hi'>
                <source>Hello</source>
                <target>Bonjour</target>
            </trans-unit>
            <trans-unit id='bye'>
                <source>Bye</source>
                <target>au revoir</target>
            </trans-unit>
        </body>
    </file>
</xliff>";
    private static readonly string _content_1_2 = @"<xliff version='1.2' xmlns='urn:oasis:names:tc:xliff:document:1.2'>
    <file original='hello.txt' source-language='en' target-language='fr' datatype='plaintext'>
        <body>
            <trans-unit id='hi'>
                <source>Hello</source>
                <target>Bonjour</target>
            </trans-unit>
            <trans-unit id='bye'>
                <source>Bye</source>
                <target>au revoir</target>
            </trans-unit>
        </body>
    </file>
</xliff>";
    private static readonly string _content_2_0 = @"<xliff xmlns='urn:oasis:names:tc:xliff:document:2.0' version='2.0' srcLang='en-US' trgLang='fr-FR'>
    <file id='f1' original='Graphic Example.psd'>
        <skeleton href='Graphic Example.psd.skl'/>
        <unit id='1'>
            <segment>
                <source>Hello</source>
                <target>Bonjour</target>
            </segment>
        </unit>
        <unit id='2'>
            <segment>
                <source>Bye</source>
                <target>au revoir</target>
            </segment>
        </unit>
    </file>
</xliff>";
    public static IEnumerable<object[]> XliffStreams
        => new List<object[]>
            {
                new object[] { CreateStream(_content_1_1) },
                new object[] { CreateStream(_content_1_2) },
                new object[] { CreateStream(_content_2_0) }
            };

    [Theory]
    [MemberData(nameof(XliffStreams))]
    public async Task Parse_ValidXliff_ReturnsCultureDictionaryRecords(Stream stream)
    {
        // Act
        var records = await XliffReader.ParseAsync(stream);

        // Assert
        Assert.NotNull(records);
        Assert.Equal(2, records.Count());
    }

    [Fact]
    public async Task Parse_InvalidXliff_ThrowsXliffException()
    {
        // Arrange
        var stream = new MemoryStream();

        // Act & Assert
        await Assert.ThrowsAsync<XliffException>(async () => await XliffReader.ParseAsync(stream));
    }

    private static Stream CreateStream(string content)
    {
        var memoryStream = new MemoryStream();
        var streamWriter = new StreamWriter(memoryStream);

        streamWriter.WriteLine(content);
        streamWriter.Flush();
        memoryStream.Seek(0, SeekOrigin.Begin);

        return memoryStream;
    }
}
