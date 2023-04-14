using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrchardCore.Environment.Shell;
using OrchardCoreContrib.Users.Services;

namespace OrchardCoreContrib.Modules.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddOrchardCms(builder =>
                {
                    builder.AddSetupFeatures("OrchardCore.AutoSetup", "OrchardCoreContrib.Tenants");
                    builder.ConfigureServices(builderServices =>
                    {
                        builderServices.AddDataMigrations();

                        builderServices.AddScoped<MigrationUpdater>();
                        builderServices.AddScoped<IFeatureEventHandler>(sp => sp.GetRequiredService<MigrationUpdater>());
                    });
                });

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
