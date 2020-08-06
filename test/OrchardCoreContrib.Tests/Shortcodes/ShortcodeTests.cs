using System.Threading.Tasks;
using OrchardCoreContrib.Shortcodes;
using OrchardCoreContrib.Shortcodes.Services;
using Shortcode = OrchardCoreContrib.Shortcodes.Shortcode;
using Shortcodes;
using Xunit;

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
        public async Task ProcessImageShortcode(string input, string expected)
        {
            // Arrange
            var imageShortcode = new ImageShortcode();
            var shortcodeService = new ShortcodeService(new IShortcodeProvider[] { imageShortcode });

            // Act
            var result = await shortcodeService.ProcessAsync(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("[bold]Hello[/bold]", "<b>Hello</b>")]
        public async Task ProcessBoldShortcode(string input, string expected)
        {
            // Arrange
            var boldShortcode = new BoldShortcode();
            var shortcodeService = new ShortcodeService(new IShortcodeProvider[] { boldShortcode });

            // Act
            var result = await shortcodeService.ProcessAsync(input);

            // Assert
            Assert.Equal(expected, result);
        }

        [ShortcodeTarget("image")]
        [ShortcodeTarget("media")]
        private class ImageShortcode : Shortcode
        {
            public override async Task ProcessAsync(ShortcodeContext context, ShortcodeOutput output)
            {
                if (context.Attributes.Count == 0)
                {
                    if (context.ShortcodeName == "media")
                    {
                        output.Content = "[image]";
                    }

                    return;
                }

                output.Content = "<img";

                if (context.Attributes.Contains("src"))
                {
                    output.Content += $" src=\"{context.Attributes.Get("src")}\"";
                }

                if (context.Attributes.Contains("width"))
                {
                    output.Content += $" width=\"{context.Attributes.Get("width")}\"";
                }

                if (context.Attributes.Contains("height"))
                {
                    output.Content += $" height=\"{context.Attributes.Get("height")}\"";
                }

                output.Content += " />";

                await Task.CompletedTask;
            }
        }

        [ShortcodeTarget("bold")]
        private class BoldShortcode : Shortcode
        {
            public override async Task ProcessAsync(ShortcodeContext context, ShortcodeOutput output)
            {
                output.Content = "<b>" + output.Content + "</b>";

                await Task.CompletedTask;
            }
        }
    }
}
