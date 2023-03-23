using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.DisplayManagement.Html;
using OrchardCore.Environment.Shell.Configuration;
using OrchardCore.Modules;
using OrchardCore.ResourceManagement;

namespace OrchardCoreContrib.Themes.Admin
{
    public class Startup : StartupBase
    {
        private readonly IShellConfiguration _configuration;

        public Startup(IShellConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionsConfiguration>();
            //services.Configure<TheAdminThemeOptions>(_configuration.GetSection("TheAdminTheme:StyleSettings"));
        }
    }
}
