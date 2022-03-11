using OrchardCore.Localization;

namespace OrchardCoreContrib.Localization.Data
{
    internal class NullDataTranslationProvider : IDataTranslationProvider
    {
        public void LoadTranslations(string cultureName, CultureDictionary dictionary)
        {

        }
    }
}
