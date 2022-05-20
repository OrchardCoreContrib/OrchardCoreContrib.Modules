using System.ComponentModel.DataAnnotations;

namespace OrchardCoreContrib.GoogleMaps.ViewModels
{
    public class GoogleMapsSettingsViewModel
    {
        public string ApiKey { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
