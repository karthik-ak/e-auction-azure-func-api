using EAuction.Products.Api.Data;
using EAuction.Products.Api.Data.Abstractions;
using EAuction.Products.Api.Repositories;
using EAuction.Products.Api.Repositories.Abstractions;
using EAuction.Products.Api.Settings;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

[assembly: FunctionsStartup(typeof(EAuction.Products.AzureFunction.API.Startup))]

namespace EAuction.Products.AzureFunction.API
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<ProductDatabaseSettings>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("ProductDatabaseSettings").Bind(settings);
            });

            //builder.Services.Configure<ProductDatabaseSettings>(Configuration.GetSection(nameof(ProductDatabaseSettings)));
            builder.Services.AddSingleton<IProductDatabaseSettings, ProductDatabaseSettings>(sp => sp.GetRequiredService<IOptions<ProductDatabaseSettings>>().Value);
            builder.Services.AddTransient<IProductContext, ProductContext>();
            builder.Services.AddTransient<IProductRepository, ProductRepository>();
        }
    }
}
