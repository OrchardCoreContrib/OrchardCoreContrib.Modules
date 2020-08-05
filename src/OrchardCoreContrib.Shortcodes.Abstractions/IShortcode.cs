using System.Threading.Tasks;

namespace OrchardCoreContrib.Shortcodes
{
    /// <summary>
    /// Contract for shortcode.
    /// </summary>
    public interface IShortcode
    {
        /// <summary>
        /// Processes the shortcode and render the output.
        /// </summary>
        /// <param name="context">The <see cref="ShortcodeContext"/>.</param>
        /// <param name="output">The <see cref="ShortcodeOutput"/>.</param>
        Task ProcessAsync(ShortcodeContext context, ShortcodeOutput output);
    }
}
