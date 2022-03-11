using OrchardCoreContrib.DataLocalization.Models;
using System.Collections.Generic;

namespace OrchardCoreContrib.DataLocalization.ViewModels
{
    public class ContentTypeResourcesViewModel
    {
        public IEnumerable<string> ResourcesNames { get; set; }

        public IEnumerable<Translation> Translations { get; set; }
    }
}
