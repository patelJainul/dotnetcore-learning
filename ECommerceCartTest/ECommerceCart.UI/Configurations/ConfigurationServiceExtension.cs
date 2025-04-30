using ECommerceCart.Core.Domain.Entities;
using ECommerceCart.Core.Domain.Entities.Identity;
using ECommerceCart.Core.Domain.RepositoryContracts;
using ECommerceCart.Core.ServiceContracts;
using ECommerceCart.Core.Services;
using ECommerceCart.Infrastructure.DbContext;
using ECommerceCart.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceCart.UI.Configurations;

public static class ConfigurationServiceExtension
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddControllersWithViews();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IRepository<Cart>, Repository<Cart>>();
        services.AddScoped<IRepository<CartVsProducts>, Repository<CartVsProducts>>();
        services.AddScoped<IRepository<Product>, Repository<Product>>();

        services.AddScoped<ICartServices, CartServices>();
        services.AddScoped<IProductServices, ProductServices>();

        services
            .AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
            .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

        services
            .AddAuthorizationBuilder()
            .SetFallbackPolicy(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build())
            .AddPolicy(
                "NotAuthorized",
                policy =>
                    policy.RequireAssertion(context =>
                        context.User.Identity?.IsAuthenticated == false
                    )
            )
            .AddPolicy("Admin", policy => policy.RequireRole("Admin"))
            .AddPolicy("User", policy => policy.RequireRole("User"));

        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
            options.AccessDeniedPath = "/Account/AccessDenied";
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = TimeSpan.FromDays(30);
            options.Cookie.SameSite = SameSiteMode.Lax; // Ensures cookies work across different environments
            options.Cookie.HttpOnly = true; // Enhances security by making the cookie inaccessible to JavaScript
        });

        return services;
    }
}
