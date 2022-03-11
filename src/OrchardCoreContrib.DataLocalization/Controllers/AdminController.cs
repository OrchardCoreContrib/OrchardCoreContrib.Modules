using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Caching.Memory;
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

        private readonly IDataResourceStringProvider _dataResourceStringProvider;
        private readonly TranslationsManager _translationsManager;
        private readonly IMemoryCache _memoryCache;
        private readonly INotifier _notifier;
        private readonly IHtmlLocalizer H;

        public AdminController(
            IDataResourceStringProvider dataResourceStringProvider,
            TranslationsManager translationsManager,
            IMemoryCache memoryCache,
            IHtmlLocalizer<AdminController> htmlLocalizer,
            INotifier notifier)
        {
            _dataResourceStringProvider = dataResourceStringProvider;
            _translationsManager = translationsManager;
            _memoryCache = memoryCache;
            _notifier = notifier;
            H = htmlLocalizer;
        }

        public async Task<ActionResult> ManageContentTypeResources([FromQuery] string selectedCulture)
        {
            var resourcesNames = _dataResourceStringProvider
                .GetAllResourceStrings(ContentTypeResourceStringProvider.Context)
                .Select(r => r.GetMessageId());

            var translationsDocument = await _translationsManager.GetTranslationsDocumentAsync();

            var viewModel = new ContentTypeResourcesViewModel
            {
                ResourcesNames = resourcesNames,
                Translations = Enumerable.Empty<Translation>()
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
            var translations = new List<Translation>();

            foreach (var key in Request.Form.Keys.Where(k => !k.Equals(AntiForgeryTokenKey)))
            {
                var value = Request.Form[key].ToString();

                translations.Add(new Translation
                {
                    Context = ContentTypeResourceStringProvider.Context,
                    Key = key,
                    Value = value
                });
            }

            await _translationsManager.UpdateTranslationAsync(selectedCulture, translations);

            // Purge the resource cache
            _memoryCache.Remove(ResourcesCachePrefix + selectedCulture);

            await _notifier.SuccessAsync(H["The resource has been saved successfully."]);

            return RedirectToAction(nameof(ManageContentTypeResources), new { selectedCulture });
        }
    }
}
