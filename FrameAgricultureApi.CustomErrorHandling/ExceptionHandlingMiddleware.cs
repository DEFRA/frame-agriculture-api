using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace FrameAgricultureApi.CustomErrorHandling;

/// <summary>
/// Custom Exception handling class
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandlingMiddleware> logger;
    /// <summary>
    /// Exception handler constructor
    /// </summary>
    /// <param name="next">next error</param>
    /// <param name="logger">error logger</param>
    /// <exception cref="ArgumentNullException">null exception</exception>
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        this.next = next ?? throw new ArgumentNullException(nameof(next));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Invoge handler
    /// </summary>
    /// <param name="httpContext">http Context</param>
    /// <returns>no return</returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch(Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;

        var errorResponse = new ErrorResponse
        {
            Success = false
        };
        switch(exception)
        {
            case CustomAppException ex:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = ex.Message;
                break;
            case UnauthorizedAccessException ex:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse.Message = ex.Message;
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Message = "Internal server error with exception type: " + exception.GetType().ToString() + "!";
                break;
        }
        string innerMessage = exception.InnerException != null ? exception.InnerException.Message : "";
        logger.LogError($"Exception Message: {exception.Message}; InnerException Message: {innerMessage}");
        var result = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(result);
    }
}

