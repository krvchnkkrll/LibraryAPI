using LibraryAPI.Application;
using LibraryAPI.Infrastructure;
using Hangfire;
using Serilog; 
using System.IO.Compression;
using LibraryAPI.Application.Services.Hangfire;
using LibraryAPI.Application.Services.Logging;

using Microsoft.AspNetCore.ResponseCompression;
using Serilog.Filters;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Logger(entityLogger => entityLogger
        .Filter.ByIncludingOnly(Matching.FromSource("LibraryAPI.Infrastructure.LoggingEntities.EntityChangeLogging"))
        .WriteTo.File("Logs/entity-changes-log-.txt", rollingInterval: RollingInterval.Day))
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddControllers();

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<LoggingMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();


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