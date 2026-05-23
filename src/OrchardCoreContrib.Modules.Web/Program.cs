using OrchardCore.Environment.Shell;
using OrchardCoreContrib.Avatars;
using OrchardCoreContrib.Modules.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOrchardCms(builder =>
    {
        builder.AddSetupFeatures("OrchardCore.AutoSetup", "OrchardCoreContrib.Tenants");
        builder.ConfigureServices(builderServices =>
        {
            builderServices.AddYesSqlDataMigrations();
    
            builderServices.AddScoped<MigrationUpdater>();
            builderServices.AddScoped<IFeatureEventHandler>(sp => sp.GetRequiredService<MigrationUpdater>());
        });
    });

// Could be add to services.AddOrchardCms().WithOrchardCoreContrib()
builder.Services.AddScoped<IAvatarService, NullAvatarService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseOrchardCore();

await app.RunAsync();

namespace OrchardCoreContrib.Modules.Web
{
    public partial class Program;
}
