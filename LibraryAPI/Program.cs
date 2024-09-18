using System.Reflection;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using LibraryAPI.Behaviors;
using LibraryAPI.DbContext;
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Services.Logging;
using Microsoft.IdentityModel.Tokens;
using Hangfire;
using Hangfire.PostgreSql;
using LibraryAPI.Services.Hangfire;
using Serilog; 
using System.IO.Compression;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<LibraryInfoContext>(
    dbContextOptions => dbContextOptions.UseNpgsql(
        builder.Configuration.GetConnectionString("LibraryDB")));

builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>(); 
    options.Providers.Add<BrotliCompressionProvider>();
    options.EnableForHttps = true;
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Authentication:Issuer"],
                ValidAudience = builder.Configuration["Authentication:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]!))
            };
        }
    );

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("IsItStaff", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("IsItStaff", "True");
    });

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(options => 
        options.UseNpgsqlConnection(builder.Configuration.GetConnectionString("HangfireConnection"))));

builder.Services.AddHangfireServer();
builder.Services.AddTransient<UserBookService>();
builder.Services.AddTransient<BookService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseMiddleware<LoggingMiddleware>();

app.MapControllers();
app.MapHangfireDashboard("/hangfire"); 

app.UseResponseCompression();

app.MapFallback(async context =>
{
    context.Response.StatusCode = 404;
    await context.Response.WriteAsync("404 Page not found");
});

app.Lifetime.ApplicationStopped.Register(Log.CloseAndFlush);


var recurringJobs = app.Services.GetRequiredService<IRecurringJobManager>();
recurringJobs.ConfigureRecurringJobs(app.Services);

app.Run();