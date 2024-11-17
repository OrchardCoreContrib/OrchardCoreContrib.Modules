using Shortcodes;

namespace OrchardCoreContrib.Shortcodes.Services;

/// <summary>
/// Represents a services that handle a shortcodes.
/// </summary>
/// <remarks>
/// Creates a new instance of <see cref="ShortcodeService"/>.
/// </remarks>
/// <param name="shortcodeProviders">The <see cref="IEnumerable{IShortcodeProvider}"/>.</param>
public class ShortcodeService(IEnumerable<IShortcodeProvider> shortcodeProviders) : IShortcodeService
{
    private readonly ShortcodesProcessor _shortcodesProcessor = new(shortcodeProviders);

    /// <inheritdoc/>
    public ValueTask<string> ProcessAsync(string input) => _shortcodesProcessor.EvaluateAsync(input);
}
