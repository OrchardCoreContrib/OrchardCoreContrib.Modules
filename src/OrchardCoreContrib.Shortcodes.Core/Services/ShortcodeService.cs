using System.Collections.Generic;
using System.Threading.Tasks;
using Shortcodes;

namespace OrchardCoreContrib.Shortcodes.Services
{
    /// <summary>
    /// Represents a services that handle a shortcodes.
    /// </summary>
    public class ShortcodeService : IShortcodeService
    {
        private readonly ShortcodesProcessor _shortcodesProcessor;

        /// <summary>
        /// Creates a new instance of <see cref="ShortcodeService"/>.
        /// </summary>
        /// <param name="shortcodeProviders">The <see cref="IEnumerable{IShortcodeProvider}"/>.</param>
        public ShortcodeService(IEnumerable<IShortcodeProvider> shortcodeProviders)
        {
            _shortcodesProcessor = new ShortcodesProcessor(shortcodeProviders);
        }

        /// <inheritdoc/>
        public ValueTask<string> ProcessAsync(string input) => _shortcodesProcessor.EvaluateAsync(input);
    }
}
