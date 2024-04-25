using System.ComponentModel.DataAnnotations;

namespace OrchardCoreContrib.Sms.Azure.ViewModels;

public class AzureSmsSettingsViewModel 
{
    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public string Message { get; set; }
}
