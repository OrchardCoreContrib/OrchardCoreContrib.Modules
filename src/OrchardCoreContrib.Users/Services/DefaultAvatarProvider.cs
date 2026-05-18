using OrchardCoreContrib.Infrastructure;
using SkiaSharp;

namespace OrchardCoreContrib.Avatars;

/// <summary>
/// Provides an avatar image of the user's initials on a colored background.
/// </summary>
public class DefaultAvatarProvider : IAvatarProvider
{
    /// <inheritdoc/>
    public string GetAvatar(AvatarContext context, int size = 80)
    {
        Guard.ArgumentNotNull(context, nameof(context));
        Guard.ArgumentNotNullOrEmpty(context.DisplayName, nameof(context.DisplayName));

        var initials = string.Join("", context.DisplayName
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Take(2)
            .Select(n => n[0]) ?? ['U'])
            .ToUpperInvariant();

        using var bitmap = new SKBitmap(size, size);
        using var canvas = new SKCanvas(bitmap);

        canvas.Clear(SKColors.Red);

        using var typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold);
        using var font = new SKFont(typeface, size / 2f)
        {
            Edging = SKFontEdging.Antialias,
            Subpixel = true
        };

        using var paint = new SKPaint
        {
            Color = SKColors.White,
            IsAntialias = true
        };

        var bounds = new SKRect();
        font.MeasureText(initials, out bounds);

        var x = (size - bounds.Width) / 2f - bounds.Left;
        var y = (size - bounds.Height) / 2f - bounds.Top;

        canvas.DrawText(initials, x, y, font, paint);

        using var image = SKImage.FromBitmap(bitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        var base64 = Convert.ToBase64String(data.ToArray());

        return $"data:image/png;base64,{base64}";
    }
}
