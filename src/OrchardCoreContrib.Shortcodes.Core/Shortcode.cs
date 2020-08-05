using Shortcodes;
using System.Collections.Generic;
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
            var shortcodeContext = new ShortcodeContext(identifier, new Dictionary<string, string>(arguments));
            var shortcodeOutput = new ShortcodeOutput(identifier, new Dictionary<string, string>(arguments));

            await ProcessAsync(shortcodeContext, shortcodeOutput);

            return shortcodeOutput.Content;
        }

        /// <inheritdoc/>
        public abstract Task ProcessAsync(ShortcodeContext context, ShortcodeOutput output);
    }
}
