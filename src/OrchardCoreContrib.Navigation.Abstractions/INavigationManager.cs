using Microsoft.AspNetCore.Mvc;

namespace OrchardCoreContrib.Navigation;

public interface INavigationManager
{
    Task<IEnumerable<MenuItem>> BuildMenuAsync(string name, ActionContext context);
}
