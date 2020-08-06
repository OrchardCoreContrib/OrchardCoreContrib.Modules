using System;
using System.Threading.Tasks;
using Shortcodes;

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
            var shortcodeContent = string.Empty;
            var shortcodeContext = new ShortcodeContext(identifier, new ShortcodeAttributes(arguments));
            var shortcodeOutput = new ShortcodeOutput(identifier, new ShortcodeAttributes(arguments));
            var shortcodeTargets = Attribute.GetCustomAttributes(GetType(), typeof(ShortcodeTargetAttribute));
            if(content == null)
            {
                shortcodeOutput.Content = "[" + identifier + "]";
            }
            else
            {
                shortcodeOutput.Content = content;
            }

            if (shortcodeTargets.Length == 0)
            {
                return default(string);
            }

            foreach (var shortcodeTarget in shortcodeTargets)
            {
                if (!(shortcodeTarget as ShortcodeTargetAttribute).Name.Equals(identifier, System.StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                    //return default(string);
                }

                await ProcessAsync(shortcodeContext, shortcodeOutput);

                shortcodeContent += shortcodeOutput.Content;
            }

            return shortcodeContent;
        }

        /// <inheritdoc/>
        public abstract Task ProcessAsync(ShortcodeContext context, ShortcodeOutput output);
    }
}
