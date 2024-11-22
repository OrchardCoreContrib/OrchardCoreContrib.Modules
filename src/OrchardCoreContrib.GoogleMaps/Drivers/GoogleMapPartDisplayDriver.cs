using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCoreContrib.GoogleMaps.Models;
using OrchardCoreContrib.GoogleMaps.ViewModels;
using System.Threading.Tasks;

namespace OrchardCoreContrib.GoogleMaps.Drivers
{
    /// <summary>
    /// Represents a display driver for <see cref="GoogleMapPart"/>.
    /// </summary>
    public class GoogleMapPartDisplayDriver : ContentPartDisplayDriver<GoogleMapPart>
    {
        public override IDisplayResult Display(GoogleMapPart part, BuildPartDisplayContext context)
        {
            var settings = context.TypePartDefinition.GetSettings<GoogleMapsSettings>();

            return Initialize<GoogleMapPartViewModel>(GetDisplayShapeType(context), model =>
            {
                model.Latitude = part.Latitude;
                model.Longitude = part.Longitude;
                model.GoogleMapPart = part;
                model.ContentItem = part.ContentItem;
                model.Settings = settings;
            })
            .Location("Detail", "Content:20")
            .Location("Summary", "Meta:5");

        }

        public override IDisplayResult Edit(GoogleMapPart part, BuildPartEditorContext context)
        {
            return Initialize<GoogleMapPartViewModel>(GetEditorShapeType(context), model =>
            {
                model.Latitude = part.Latitude;
                model.Longitude = part.Longitude;
                model.GoogleMapPart = part;
                model.ContentItem = part.ContentItem;
                model.Settings = context.TypePartDefinition.GetSettings<GoogleMapsSettings>();
            });
        }

        public override async Task<IDisplayResult> UpdateAsync(GoogleMapPart model, UpdatePartEditorContext context)
        {
            await context.Updater.TryUpdateModelAsync(model, Prefix, p => p.Latitude, p => p.Longitude);

            return Edit(model, context);
        }
    }
}
