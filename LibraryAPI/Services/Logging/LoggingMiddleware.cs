using Serilog;

namespace LibraryAPI.Services.Logging;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            var queryParams = context.Request.QueryString.HasValue ? context.Request.QueryString.Value : "none";

            var routeParams = string.Join(", ", context.Request.RouteValues.Select(rv => $"{rv.Key}: {rv.Value}"));
            
            context.Request.EnableBuffering();
            string body;
            using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
            {
                body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }
            
            Log.Information("Запрос: {Method} {Url}, параметры: {QueryParams}, тело запроса {Body}" ,
                context.Request.Method, context.Request.Path, queryParams, body);

            await _next(context);
            
            Log.Information("Запрос {Method} {Url} выполнен", 
                context.Request.Method, context.Request.Path);
            
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                Log.Warning("Запрос {Method} {Url} не выполнен, статус код ошибки 404 Not Found", 
                    context.Request.Method, context.Request.Path);
            }
        }
        catch (Exception e)
        {
            Log.Error(e, "Ошибка при выполнении запроса {Method} {Url}",
                context.Request.Method, context.Request.Path);
            
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            if (context.RequestServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
            {
                await context.Response.WriteAsync(e.ToString());
            }
            else
            {
                await context.Response.WriteAsync("Ошибка, статус код 500 Server Error");
            }
        }
    }
}