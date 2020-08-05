using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrchardCoreContrib.Shortcodes.Services;
using Shortcode = OrchardCoreContrib.Shortcodes.Shortcode;
using Shortcodes;
using Xunit;
using OrchardCoreContrib.Shortcodes;
using System.Linq;

namespace OrchardCoreContrib.Tests.Shortcodes
{
    public class ShortcodeTests
    {
        [Theory]
        [InlineData("foo bar baz", "foo bar baz")]
        [InlineData("[imageX src=\"1.jpg\"]", "")]
        [InlineData("[image]", "[image]")]
        [InlineData("[media]", "[image]")]
        [InlineData("[image src=\"1.jpg\"]", "<img src=\"1.jpg\" />")]
        [InlineData("[media src=\"1.jpg\"]", "<img src=\"1.jpg\" />")]
        [InlineData("[image src=\"1.jpg\" width=\"40\" height=\"30\"]", "<img src=\"1.jpg\" width=\"40\" height=\"30\" />")]
        [InlineData("[media src=\"1.jpg\" width=\"40\" height=\"30\"]", "<img src=\"1.jpg\" width=\"40\" height=\"30\" />")]
        [InlineData("[image src=\"1.jpg\"] <br/> [media src=\"1.jpg\"]", "<img src=\"1.jpg\" /> <br/> <img src=\"1.jpg\" />")]
        public async Task ProcessShortcode(string input, string expected)
        {
            // Arrange
            var imageShortcode = new ImageShortcode();
            var shortcodeService = new ShortcodeService(new IShortcodeProvider[] { imageShortcode });

            // Act
            var result = await shortcodeService.ProcessAsync(input);

            // Assert
            Assert.Equal(expected, result);
        }

        private class ImageShortcode : Shortcode
        {
            private static readonly HashSet<string> _shortcodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "image",
                "media"
            };

            public override async Task ProcessAsync(ShortcodeContext context, ShortcodeOutput output)
            {
                if (!_shortcodes.Contains(context.ShortcodeName))
                {
                    return;
                }

                if (!context.Attributes.Any())
                {
                    output.Content = "[image]";

                    return;
                }

                output.Content = "<img";

                if (context.Attributes.ContainsKey("src"))
                {
                    output.Content += $" src=\"{context.Attributes["src"]}\"";
                }

                if (context.Attributes.ContainsKey("width"))
                {
                    output.Content += $" width=\"{context.Attributes["width"]}\"";
                }

                if (context.Attributes.ContainsKey("height"))
                {
                    output.Content += $" height=\"{context.Attributes["height"]}\"";
                }

                output.Content += " />";

                await Task.CompletedTask;
            }
        }
    }
}
