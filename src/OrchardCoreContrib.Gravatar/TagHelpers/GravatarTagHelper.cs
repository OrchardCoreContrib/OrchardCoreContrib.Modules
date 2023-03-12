using Microsoft.AspNetCore.Razor.TagHelpers;
using OrchardCoreContrib.Gravatar.Services;

namespace OrchardCoreContrib.Gravatar.TagHelpers;

[HtmlTargetElement("gravatar", TagStructure = TagStructure.NormalOrSelfClosing)]
public class GravatarTagHelper : TagHelper
{
    private readonly IGravatarService _gravatarService;

    public GravatarTagHelper(IGravatarService gravatarService)
    {
        _gravatarService = gravatarService;
    }

    public string Email { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "img";

        var imageUrl = _gravatarService.GetAvatarUrl(Email);

        output.Attributes.Add("src", imageUrl);
    }
}
