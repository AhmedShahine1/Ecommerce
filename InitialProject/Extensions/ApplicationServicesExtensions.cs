using Ecommerce.BusinessLayer.Interfaces;
using Ecommerce.BusinessLayer.Services;
using System.Reflection;
using Ecommerce.BusinessLayer.AutoMapper; // Explicit namespace

namespace Ecommerce.Extensions;

public static class ApplicationServicesExtensions
{
    // interfaces sevices [IAccountService, IPhotoHandling  ]
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config, params Assembly[] assemblies)
    {
        services.AddDistributedMemoryCache(); // Add this line to configure the distributed cache

        // Session Service
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromHours(12);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        services.AddScoped<IAccountService, AccountService>();
        services.AddTransient<IFileHandling, FileHandling>();
        services.AddHttpClient();
        services.AddAutoMapper(typeof(MappingProfile));
        return services;
    }

    public static IApplicationBuilder UseApplicationMiddleware(this IApplicationBuilder app)
    {
        app.UseSession();
        /*   app.UseHangfireDashboard("/Hangfire/Dashboard");

           app.UseWebSockets();*/

        return app;
    }
}