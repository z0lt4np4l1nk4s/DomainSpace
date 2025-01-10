namespace DomainSpace.WebApi.Infrastructure.Middleware;

/// <summary>
/// Exception handling middleware
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="next">Request delegate</param>
    /// <param name="logger">Logger</param>
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invoke method
    /// </summary>
    /// <param name="context">Http context</param>
    /// <returns>Task</returns>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Method for handling exceptions
    /// </summary>
    /// <param name="context">Http context</param>
    /// <param name="exception">Exception</param>
    /// <returns>Task</returns>
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        
        var result = JsonSerializer.Serialize(ServiceResult.Failure(new ErrorMessage
        {
            Description = "Internal server error",
            ErrorCode = "500"
        }), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

        return context.Response.WriteAsync(result);
    }
}
