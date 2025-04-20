using CitiesManager.Infrastructure.DatabaseContext;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.ServiceContracts.Cities;
using ContactsManager.Core.Services.Cities;
using ContactsManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CitiesManager.WebAPI.ConfigureServicesExtension;

public static class ConfigureServicesExtension
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddControllers();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );
        services.AddEndpointsApiExplorer();
        services.AddScoped<ICityRepository, CitiesRepository>();
        services.AddScoped<ICityAddServices, CityAddServices>();
        services.AddScoped<ICityGetServices, CitiesGetServices>();
        services.AddScoped<ICityUpdateServices, CityUpdateServices>();
        services.AddScoped<ICityDeleteServices, CityDeleteServices>();

        return services;
    }
}
