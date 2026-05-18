using Microsoft.AspNetCore.Server.Kestrel.Core;
using OrchardCoreContrib.Avatars;

namespace OrchardCoreContrib.Modules.Web;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Fix IOE Synchronous operations are disallowed
        services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
        services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);

        services
            .AddOrchardCms(builder =>
            {
                builder.AddSetupFeatures("OrchardCore.AutoSetup", "OrchardCoreContrib.Tenants");
                //builder.ConfigureServices(builderServices =>
                //{
                //    builderServices.AddYesSqlDataMigrations();

                //    builderServices.AddScoped<MigrationUpdater>();
                //    builderServices.AddScoped<IFeatureEventHandler>(sp => sp.GetRequiredService<MigrationUpdater>());
                //});
            });

        // Could be add to services.AddOrchardCms().WithOrchardCoreContrib()
        services.AddScoped<IAvatarProvider, NullAvatarProvider>();
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
