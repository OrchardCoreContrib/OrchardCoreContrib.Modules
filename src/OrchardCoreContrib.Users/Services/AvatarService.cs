using Microsoft.Extensions.Options;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace OrchardCoreContrib.Users.Services;

public class AvatarService(IOptions<AvatarOptions> avatarOptions) : IAvatarService
{
    private readonly AvatarOptions _avatarOptions = avatarOptions.Value;

    public string Generate(string userName)
    {
        var width = 30;
        var height = 30;
        using (var image = new Image<Rgba32>(width, height))
        {
            _ = Color.TryParseHex(_avatarOptions.BackColor, out var backColor);
            _ = Color.TryParseHex(_avatarOptions.ForeColor, out var foreColor);

            var yourPolygon = new EllipsePolygon(x: width / 2, y: height / 2, width: width, height: height);
            image.Mutate(x => x.Fill(backColor, yourPolygon));

            var font = SystemFonts.CreateFont("Segoe UI", 16, FontStyle.Regular);
            image.Mutate(x => x.DrawText(userName[0].ToString().ToUpper(), font, foreColor, new PointF(width / 3, height / 6)));

            var imreBase64Data = ToBase64String(image);

            return string.Format("data:image/png;base64,{0}", imreBase64Data);
        }
    }

    private static string ToBase64String(Image<Rgba32> image)
    {
        using (var memoryStream = new MemoryStream())
        {
            var imageEncoder = image.Configuration.ImageFormatsManager.GetEncoder(PngFormat.Instance);
            
            image.Save(memoryStream, imageEncoder);

            var byteData = memoryStream.ToArray();

            return Convert.ToBase64String(byteData);
        }
    }
}
