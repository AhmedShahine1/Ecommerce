using Ecommerce.Core.Entity.ApplicationData;
using Ecommerce.Core.Helpers;
using Ecommerce.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public static class IdentityServicesExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequiredLength = 6;
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();

        services.Configure<Jwt>(config.GetSection("JWT"));

        services.AddAuthentication()
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = config["JWT:Issuer"],
                ValidAudience = config["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]))
            };
        });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin", "Support Developer"));
            options.AddPolicy("Support Developer", policy => policy.RequireRole("Support Developer"));
            options.AddPolicy("Guest", policy => policy.RequireRole("Guest", "Admin", "Support Developer"));
            options.AddPolicy("Support Staff", policy => policy.RequireRole("Support Staff", "Admin", "Support Developer", "Customer", "Vendor", "Delivery Personnel"));
            options.AddPolicy("Customer", policy => policy.RequireRole("Customer", "Admin", "Support Developer"));
            options.AddPolicy("Vendor", policy => policy.RequireRole("Vendor", "Admin", "Support Developer"));
            options.AddPolicy("Delivery Personnel", policy => policy.RequireRole("Delivery Personnel", "Admin", "Support Developer"));
        });

        return services;
    }
}
