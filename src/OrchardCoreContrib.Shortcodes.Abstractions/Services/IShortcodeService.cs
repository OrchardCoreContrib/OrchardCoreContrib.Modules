using System.Threading.Tasks;

namespace OrchardCoreContrib.Shortcodes.Services
{
    /// <summary>
    /// Contract for a service that handle a shortcodes.
    /// </summary>
    public interface IShortcodeService
    {
        /// <summary>
        /// Processes a shortcode with a given input.
        /// </summary>
        /// <param name="input">The input that contains a shortcode to render.</param>
        /// <returns>The <see cref="string"/> after manipulating the shortcode.</returns>
        ValueTask<string> ProcessAsync(string input);
    }
}
