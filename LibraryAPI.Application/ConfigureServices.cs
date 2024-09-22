using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.PostgreSql;
using LibraryAPI.Application.Behaviors;
using LibraryAPI.Application.Services.Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LibraryAPI.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var hangfireConnection = configuration.GetConnectionString("HangfireConnection");
        var hangfirePassword = configuration["DBPassword:LibraryDBPassword"];
        var authenticationSecretForKey = configuration["Authentication:SecretForKey"];
        
        hangfireConnection += hangfirePassword;
        
        services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Authentication:Issuer"],
                        ValidAudience = configuration["Authentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(authenticationSecretForKey!))
                    };
                }
            );
        
        services.AddAuthorizationBuilder()
            .AddPolicy("IsItStaff", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("IsItStaff", "True");
            });
        
        services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();
        
        services.AddHangfire(con=>con
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(options => 
                options.UseNpgsqlConnection(hangfireConnection)));
        
        services.AddHangfireServer();
        services.AddTransient<UserBookService>();
        services.AddTransient<BookService>();
        
        return services;
    }
}