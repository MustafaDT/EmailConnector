using Microsoft.AspNetCore.Http;
using System.Net;
using ECon.Application.Dtos;
using ECon.Application.Interfaces.IRepositories;
using ECon.Application.Utils;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;

namespace ECon.Application.Middleware;

public class AppGuidValidationMiddleware
{
    private readonly RequestDelegate _next;

    public AppGuidValidationMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    //public async Task Invoke(HttpContext context, ILoggingRepository loggingRepository)
    //{
    //    context.Request.Headers.TryGetValue(RequestHeaderKeys.AppGuid, out var appGuid);

    //    var applicationGuid = await loggingRepository.GetApplicationGuid(appGuid);

    //    if (applicationGuid is null)
    //    {
    //        await SetBadRequestResponse(context);
    //        return;
    //    }

    //    await _next(context);
    //}

    private static async Task SetBadRequestResponse(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";

        var errorResponse = ApiResponse.Failure(0, "Invalid value in parameter AppGuid.",  "","");
        var errorResponseJson = JsonConvert.SerializeObject(errorResponse);

        await context.Response.WriteAsync(errorResponseJson);
    }
}

public static class AppGuidValidationMiddlewareExtensions
{
    public static IApplicationBuilder UseAppGuidValidation(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AppGuidValidationMiddleware>();
    }
}