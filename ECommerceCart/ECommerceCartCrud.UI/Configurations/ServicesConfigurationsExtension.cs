using ECommerceCartCrud.Core.Domain.Entities;
using ECommerceCartCrud.Core.Domain.RepositoryContracts;
using ECommerceCartCrud.Core.ServiceContracts;
using ECommerceCartCrud.Core.Services;
using ECommerceCartCrud.Infrastructure.DBContext;
using ECommerceCartCrud.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace ECommerceCartCrud.UI.Configurations;

public static class ServicesConfigurationsExtension
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
        services.AddScoped<IRepository<Product>, Repository<Product>>();

        services.AddScoped<ICartServices, CartServices>();
        services.AddScoped<IProductServices, ProductServices>();
        return services;
    }
}
