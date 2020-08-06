using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
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
            var shortcodeOutput = new ShortcodeOutput(new ShortcodeAttributes(arguments));
            var shortcodeTargets = Attribute.GetCustomAttributes(GetType(), typeof(ShortcodeTargetAttribute))
                .Select(t => t as ShortcodeTargetAttribute)
                .ToArray();
            if(content == null)
            {
                shortcodeOutput.Content = "[" + identifier + "]";
            }
            else
            {
                shortcodeOutput.Content = content;
            }

            if (shortcodeTargets.Length == 0 || shortcodeTargets.All(t => !t.Name.Equals(identifier, StringComparison.OrdinalIgnoreCase)))
            {
                return default;
            }

            foreach (var shortcodeTarget in shortcodeTargets)
            {
                if (!(shortcodeTarget.Name.Equals(identifier, StringComparison.OrdinalIgnoreCase)))
                {
                    continue;
                }

                await ProcessAsync(shortcodeContext, shortcodeOutput);

                shortcodeContent += shortcodeOutput.Content;
            }

            var stringWriter = new StringWriter();
            shortcodeOutput.WriteTo(stringWriter, NullHtmlEncoder.Default);

            return stringWriter.GetStringBuilder().ToString();
        }

        /// <inheritdoc/>
        public abstract Task ProcessAsync(ShortcodeContext context, ShortcodeOutput output);
    }
}
