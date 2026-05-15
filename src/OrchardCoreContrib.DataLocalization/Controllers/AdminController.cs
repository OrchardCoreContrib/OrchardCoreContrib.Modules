using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Caching.Memory;
using OrchardCore.ContentTypes.Services;
using OrchardCore.DisplayManagement.Notify;
using OrchardCoreContrib.DataLocalization.Models;
using OrchardCoreContrib.DataLocalization.Services;
using OrchardCoreContrib.DataLocalization.ViewModels;
using OrchardCoreContrib.Localization;
using OrchardCoreContrib.Localization.Data;

namespace OrchardCoreContrib.DataLocalization.Controllers;

public class AdminController(
    IContentDefinitionService contentDefinitionService,
    IEnumerable<IDataResourceStringProvider> dataResourceStringProviders,
    TranslationsManager translationsManager,
    IMemoryCache memoryCache,
    IHtmlLocalizer<AdminController> H,
    INotifier notifier) : Controller
{
    private const string ResourcesCachePrefix = "OCC-CultureDictionary-";
    private const string AntiForgeryTokenKey = "__RequestVerificationToken";

    public async Task<ActionResult> ManageContentTypeResources([FromQuery] string selectedCulture)
    {
        var resourcesNames = await GetResourcesNamesAsync(ContentTypeResourceStringProvider.Context);

        var translationsDocument = await translationsManager.GetTranslationsDocumentAsync();

        var viewModel = new ContentTypeResourcesViewModel
        {
            ResourcesNames = resourcesNames,
            Translations = [],
            SelectedCulture = selectedCulture
        };

        if (!string.IsNullOrEmpty(selectedCulture) &&
            translationsDocument.Translations.TryGetValue(selectedCulture, out IEnumerable<Translation> value))
        {
            viewModel.Translations = value;
        }

        return View(viewModel);
    }

    [HttpPost]
    [ActionName(nameof(ManageContentTypeResources))]
    public async Task<ActionResult> ManageContentTypeResourcesPost([FromQuery] string selectedCulture)
    {
        await UpdateResourcesAsync(ContentTypeResourceStringProvider.Context, selectedCulture);

        return RedirectToAction(nameof(ManageContentTypeResources), new { selectedCulture });
    }

    public async Task<ActionResult> ManageContentFieldResources([FromQuery] string selectedCulture, [FromQuery] string contentType)
    {
        var context = $"{contentType}-{ContentFieldResourceStringProvider.Context}";
        var resourcesNames = await GetResourcesNamesAsync(context);

        var translationsDocument = await translationsManager.GetTranslationsDocumentAsync();

        var viewModel = new ContentFieldResourcesViewModel
        {
            ContentTypes = (await contentDefinitionService.GetTypesAsync()).Select(t => t.Name),
            ResourcesNames = resourcesNames,
            Translations = [],
            SelectedContentType = contentType,
            SelectedCulture = selectedCulture
        };

        if (!string.IsNullOrEmpty(selectedCulture) &&
            translationsDocument.Translations.TryGetValue(selectedCulture, out IEnumerable<Translation> value))
        {
            viewModel.Translations = value;
        }

        return View(viewModel);
    }

    [HttpPost]
    [ActionName(nameof(ManageContentFieldResources))]
    public async Task<ActionResult> ManageContentFieldResourcesPost([FromQuery] string selectedCulture, [FromQuery] string contentType)
    {
        var context = $"{contentType}-{ContentFieldResourceStringProvider.Context}";
        
        await UpdateResourcesAsync(context, selectedCulture);

        return RedirectToAction(nameof(ManageContentFieldResources), new { selectedCulture, contentType });
    }

    private async Task<IEnumerable<string>> GetResourcesNamesAsync(string context)
    {
        IEnumerable<string> resourcesNames = null;
        foreach (var dataResourceStringProvider in dataResourceStringProviders)
        {
            resourcesNames = (await dataResourceStringProvider.GetAllResourceStringsAsync(context))
                .Select(r => r.GetMessageId());

            if (resourcesNames.Any())
            {
                break;
            }
        }

        return resourcesNames;
    }

    private async Task UpdateResourcesAsync(string context, string culture)
    {
        var translations = new List<Translation>();

        var translationsDocument = await translationsManager.GetTranslationsDocumentAsync();

        if (translationsDocument.Translations.TryGetValue(culture, out IEnumerable<Translation> translationsValue))
        {
            translations = [.. translationsValue];
        }

        foreach (var key in Request.Form.Keys.Where(k => !k.Equals(AntiForgeryTokenKey)))
        {
            var value = Request.Form[key].ToString();
            var index = translations.FindIndex(t => t.Context == context && t.Key == key);

            if (index > -1)
            {
                translations[index].Value = value;
            }
            else
            {
                translations.Add(new Translation
                {
                    Context = context,
                    Key = key,
                    Value = value
                });
            }
        }

        await translationsManager.UpdateTranslationAsync(culture, translations);

        // Purge the resource cache
        memoryCache.Remove(ResourcesCachePrefix + culture);

        await notifier.SuccessAsync(H["The resource has been saved successfully."]);
    }
}
