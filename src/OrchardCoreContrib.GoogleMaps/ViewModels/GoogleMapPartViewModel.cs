using Microsoft.AspNetCore.Mvc.ModelBinding;
using OrchardCore.ContentManagement;
using OrchardCoreContrib.GoogleMaps.Models;

namespace OrchardCoreContrib.GoogleMaps.ViewModels;

public class GoogleMapPartViewModel
{
    public double Latitude { get; set; }

    public double Longitude { get; set; }

    [BindNever]
    public ContentItem ContentItem { get; set; }

    [BindNever]
    public GoogleMapPart GoogleMapPart { get; set; }

    [BindNever]
    public GoogleMapsSettings Settings { get; set; }

    public bool DevelopmentMode => String.IsNullOrEmpty(Settings.ApiKey);
}
