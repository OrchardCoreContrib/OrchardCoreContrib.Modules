using OrchardCore.ContentManagement;

namespace OrchardCoreContrib.GoogleMaps.Models;

public class GoogleMapPart : ContentPart
{
    public double Latitude { get; set; }

    public double Longitude { get; set; }
}
