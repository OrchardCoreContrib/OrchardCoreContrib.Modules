using System.Collections.Generic;
using System.Threading.Tasks;
using OrchardCoreContrib.Shortcodes.Services;
using Shortcodes;
using Xunit;

namespace OrchardCoreContrib.Tests.Shortcodes
{
    public class ShortcodeServiceTests
    {
        private readonly IEnumerable<IShortcodeProvider> _shortcodeProviders;

        public ShortcodeServiceTests()
        {
            _shortcodeProviders = new List<IShortcodeProvider> {
                new NamedShortcodeProvider
                {
                    ["hello"] = (args, content, ctx) => new ValueTask<string>("Hello world!"),
                    ["upper"] = (args, content, ctx) => new ValueTask<string>(content.ToUpperInvariant())
                }
            };
        }

        [Theory]
        [InlineData("[hello]", "Hello world!")]
        [InlineData("[upper]Orchard Core Contrib[/upper]", "ORCHARD CORE CONTRIB")]
        public async Task ProcessShortcode(string input, string expected)
        {
            // Arrange
            var shortcodeService = new ShortcodeService(_shortcodeProviders);

            // Act
            var result = await shortcodeService.ProcessAsync(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
