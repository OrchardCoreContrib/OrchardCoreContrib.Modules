using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrchardCoreContrib.Users.Services;

namespace OrchardCoreContrib.Modules.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddOrchardCms()
                .AddSetupFeatures("OrchardCore.AutoSetup", "OrchardCoreContrib.Tenants");

            // Workaround to avoid IOE on UserMenu shape
            services.AddScoped<IAvatarService, NullAvatarService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOrchardCore();
        }
    }
}
