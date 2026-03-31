using LuniShop.Domain.Exceptions;

namespace LuniShop.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next; // 
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger) 
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(DomainException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(new { Message = ex.Message });
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { Message = "Error has ocured" });
        }
    }
}
