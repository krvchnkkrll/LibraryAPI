using LibraryAPI.Infrastructure.DbContext;
using LibraryAPI.Infrastructure.LoggingEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryAPI.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,  IConfiguration configuration)
    {
        var libraryDbConnection = configuration.GetConnectionString("LibraryDB");
        var libraryDbPassword = configuration["DBPassword:LibraryDBPassword"];

        libraryDbConnection += libraryDbPassword; 
        
        services.AddDbContext<LibraryInfoContext>(dbContextOptions =>
        {
            dbContextOptions.UseNpgsql(libraryDbConnection);
        });
        
        services.AddTransient<EntityChangeLogging>();
        
        return services;
    }
}

