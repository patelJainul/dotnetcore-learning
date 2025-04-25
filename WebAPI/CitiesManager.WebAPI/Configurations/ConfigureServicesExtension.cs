using System.Text;
using CitiesManager.Infrastructure.DatabaseContext;
using ContactsManager.Core.Domain.RepositoryContracts;
using ContactsManager.Core.Identity;
using ContactsManager.Core.ServiceContracts.Cities;
using ContactsManager.Core.ServiceContracts.JWT;
using ContactsManager.Core.Services.Cities;
using ContactsManager.Core.Services.JWT;
using ContactsManager.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CitiesManager.WebAPI.ConfigureServicesExtension;

/// <summary>
/// The `ConfigureServicesExtension` class contains an extension method for configuring services in an ASP.NET Core
/// application.
/// It is used to set up the dependency injection container with various services required by the application.
/// </summary>
public static class ConfigureServicesExtension
{
    /// <summary>
    /// This extension method configures the services for the ASP.NET Core application.
    /// It adds the necessary services
    /// for controllers, database context, and various city-related services.
    /// </summary>
    /// <param name="services">
    /// The `IServiceCollection` instance that is used to register services in the ASP.NET Core dependency
    /// injection container.
    /// </param>
    /// <param name="configuration">
    /// The `IConfiguration` instance that provides access to the application's configuration settings,
    /// including connection strings and other
    /// configuration values.
    /// </param>
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddControllers(options =>
        {
            options.Filters.Add(new ProducesAttribute("application/json"));
            // var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            // options.Filters.Add(new AuthorizeFilter(policy));
        });

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );

        // api versioning
        services.AddApiVersioning(configuration =>
        {
            configuration.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        // swagger
        services.AddEndpointsApiExplorer();
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });
        services.AddSwaggerGen(options =>
        {
            options.IncludeXmlComments(
                Path.Combine(AppContext.BaseDirectory, "CitiesManager.WebAPI.xml")
            );
            options.SwaggerDoc(
                "v1",
                new OpenApiInfo { Title = "Cities Manager API", Version = "v1" }
            );
            options.SwaggerDoc(
                "v2",
                new OpenApiInfo { Title = "Cities Manager API", Version = "v2" }
            );

            options.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "Enter 'Bearer' followed by your JWT token. Example: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...",
                }
            );

            options.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                        },
                        Array.Empty<string>()
                    },
                }
            );
        });

        // identity
        services
            .AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
            .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

        // token services
        services.AddTransient<IAuthServices, AuthServices>();

        // dependency injection
        services.AddScoped<ICityRepository, CitiesRepository>();
        services.AddScoped<ICityAddServices, CityAddServices>();
        services.AddScoped<ICityGetServices, CitiesGetServices>();
        services.AddScoped<ICityUpdateServices, CityUpdateServices>();
        services.AddScoped<ICityDeleteServices, CityDeleteServices>();

        // authentication
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            configuration["JWT:SecretKey"]
                                ?? throw new InvalidOperationException(
                                    "JWT:SecretKey is missing in configuration."
                                )
                        )
                    ),
                    ClockSkew = TimeSpan.Zero,
                };
            });

        services.AddAuthorization();

        return services;
    }
}
