namespace OrchardCoreContrib.GoogleMaps;

public class GoogleMapsSettings
{
    public string ApiKey { get; set; }

    public double Latitude { get; set; } = GoogleMapsDefaults.Latitude;

    public double Longitude { get; set; } = GoogleMapsDefaults.Longitude;
}
