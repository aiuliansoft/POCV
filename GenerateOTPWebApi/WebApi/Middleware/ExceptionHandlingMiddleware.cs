using Newtonsoft.Json;
using System.Net.Mime;
using WebApi.Models;

namespace WebApi.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    /// <summary>
    /// Creates a new instance of <see cref="ExceptionHandlingMiddleware"/>
    /// </summary>
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (context.Response.HasStarted)
            {
                _logger.LogDebug("The response has already started, the http status code middleware will not be executed.");
                throw;
            }

            context.Response.ContentType = MediaTypeNames.Application.Json;

            // If the logic would have been a bit more complex, perhaps other exception types would be here as well.

            var responseBody = new GenericError("An internal exception occurred");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            _logger.LogError("{Exception} {ExceptionMessage}", ex, ex.Message);

            await context.Response.WriteAsync(JsonConvert.SerializeObject(responseBody));
        }
    }
}
