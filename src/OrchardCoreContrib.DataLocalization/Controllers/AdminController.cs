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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrchardCoreContrib.DataLocalization.Controllers
{
    public class AdminController : Controller
    {
        private const string ResourcesCachePrefix = "OCC-CultureDictionary-";
        private const string AntiForgeryTokenKey = "__RequestVerificationToken";

        private readonly IContentDefinitionService _contentDefinitionService;
        private readonly IEnumerable<IDataResourceStringProvider> _dataResourceStringProviders;
        private readonly TranslationsManager _translationsManager;
        private readonly IMemoryCache _memoryCache;
        private readonly INotifier _notifier;
        private readonly IHtmlLocalizer H;

        public AdminController(
            IContentDefinitionService contentDefinitionService,
            IEnumerable<IDataResourceStringProvider> dataResourceStringProviders,
            TranslationsManager translationsManager,
            IMemoryCache memoryCache,
            IHtmlLocalizer<AdminController> htmlLocalizer,
            INotifier notifier)
        {
            _contentDefinitionService = contentDefinitionService;
            _dataResourceStringProviders = dataResourceStringProviders;
            _translationsManager = translationsManager;
            _memoryCache = memoryCache;
            _notifier = notifier;
            H = htmlLocalizer;
        }

        public async Task<ActionResult> ManageContentTypeResources([FromQuery] string selectedCulture)
        {
            var resourcesNames = GetResourcesNames(ContentTypeResourceStringProvider.Context);

            var translationsDocument = await _translationsManager.GetTranslationsDocumentAsync();

            var viewModel = new ContentTypeResourcesViewModel
            {
                ResourcesNames = resourcesNames,
                Translations = Enumerable.Empty<Translation>(),
                SelectedCulture = selectedCulture
            };

            if (!String.IsNullOrEmpty(selectedCulture) && translationsDocument.Translations.ContainsKey(selectedCulture))
            {
                viewModel.Translations = translationsDocument.Translations[selectedCulture];
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
            var resourcesNames = GetResourcesNames(context);

            var translationsDocument = await _translationsManager.GetTranslationsDocumentAsync();

            var viewModel = new ContentFieldResourcesViewModel
            {
                ContentTypes = _contentDefinitionService.GetTypes().Select(t => t.Name),
                ResourcesNames = resourcesNames,
                Translations = Enumerable.Empty<Translation>(),
                SelectedContentType = contentType,
                SelectedCulture = selectedCulture
            };

            if (!String.IsNullOrEmpty(selectedCulture) && translationsDocument.Translations.ContainsKey(selectedCulture))
            {
                viewModel.Translations = translationsDocument.Translations[selectedCulture];
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

        private IEnumerable<string> GetResourcesNames(string context)
        {
            IEnumerable<string> resourcesNames = null;
            foreach (var dataResourceStringProvider in _dataResourceStringProviders)
            {
                resourcesNames = dataResourceStringProvider
                    .GetAllResourceStrings(context)
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

            var translationsDocument = await _translationsManager.GetTranslationsDocumentAsync();

            translations = translationsDocument.Translations[culture].ToList();

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

            await _translationsManager.UpdateTranslationAsync(culture, translations);

            // Purge the resource cache
            _memoryCache.Remove(ResourcesCachePrefix + culture);

            await _notifier.SuccessAsync(H["The resource has been saved successfully."]);
        }
    }
}
