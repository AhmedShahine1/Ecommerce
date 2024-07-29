using Ecommerce.BusinessLayer.Interfaces;
using Ecommerce.BusinessLayer.Services;
using Ecommerce.Core;
using Ecommerce.Core.DTO;
using Ecommerce.RepositoryLayer.Interfaces;
using Ecommerce.RepositoryLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Ecommerce.Extensions;

public static class ContextServicesExtensions
{
    public static IServiceCollection AddContextServices(this IServiceCollection services, IConfiguration config)
    {
        //- context && json services
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));//,b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)).UseLazyLoadingProxies());
        services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddControllersWithViews();
        services.AddSingleton<IRequestResponseService, RequestResponseService>();
        // IBaseRepository && IUnitOfWork Service
        //services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>)); // only Repository
        services.AddTransient<IUnitOfWork, UnitOfWork>(); // Repository and UnitOfWork

        return services;
    }
}