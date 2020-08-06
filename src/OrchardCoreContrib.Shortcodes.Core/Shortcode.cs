using Shortcodes;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Shortcodes
{
    /// <summary>
    /// Represents a base class for a shortcode.
    /// </summary>
    public abstract class Shortcode : IShortcode, IShortcodeProvider
    {
        /// <inheritdoc/>
        public async ValueTask<string> EvaluateAsync(string identifier, Arguments arguments, string content, Context context)
        {
            var shortcodeContext = new ShortcodeContext(identifier, new ShortcodeAttributes(arguments));
            var shortcodeOutput = new ShortcodeOutput(identifier, new ShortcodeAttributes(arguments));
            if (shortcodeContext.Attributes.Count == 0)
            {
                shortcodeOutput.Content = "[" +  identifier + "]";
            }

            await ProcessAsync(shortcodeContext, shortcodeOutput);

            return shortcodeOutput.Content;
        }

        /// <inheritdoc/>
        public abstract Task ProcessAsync(ShortcodeContext context, ShortcodeOutput output);
    }
}
