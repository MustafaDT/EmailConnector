using System.Net;
using ECon.Application.Dtos;
using ECon.Application.Interfaces.IRepositories;
using ECon.Application.Interfaces.IServices;
using ECon.Application.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ECon.Application.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;
 
    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext, ILoggingRepository loggingRepository , IEmailService emailService)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, loggingRepository , emailService);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex, ILoggingRepository loggingRepository , IEmailService emailService)
    {
        
        _logger.LogError($"Exception: {ex}");
        InsertExceptionInDb(context, ex, loggingRepository);

        await ReturnInternalServerErrorResponse(context, ex);
    }

    private void InsertExceptionInDb(HttpContext context, Exception ex, ILoggingRepository loggingRepository)
    {
        context.Request.Headers.TryGetValue(RequestHeaderKeys.SystemRequestId, out var requestIdStr);
        context.Request.Headers.TryGetValue(RequestHeaderKeys.AppGuid, out var appGuId);
        int.TryParse(requestIdStr, out var requestId);

        loggingRepository.InsertSystemException(appGuId, ex.GetType().ToString(), ex.Message, ex.StackTrace, false, ex.ToString(), requestId);
    }

    private static async Task ReturnInternalServerErrorResponse(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var errorResponse = ApiResponse.Failure(0, ex.Message,  "","");
        var errorResponseJson = JsonConvert.SerializeObject(errorResponse);

        await context.Response.WriteAsync(errorResponseJson);
    }
}

public static class GlobalExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}