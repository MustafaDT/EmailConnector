using ECon.Application.Interfaces.IRepositories;
using ECon.Application.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Primitives;

namespace WathqService.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task Invoke(HttpContext context, ILoggingRepository loggingRepository)
    {
        await LogRequest(context.Request, loggingRepository);
        await _next(context);
    }

    private async Task LogRequest(HttpRequest request, ILoggingRepository loggingRepository)
    {
        request.Headers.TryGetValue(RequestHeaderKeys.AppGuid, out var appGuid);
        request.Headers.TryGetValue(RequestHeaderKeys.UserIpAddress, out var userIpAddress);
        var clientIdentifier = new StringValues();
        
        request.Query.TryGetValue("crNo", out clientIdentifier);
        
        var requestId = await loggingRepository.InsertSystemRequest(action: request.Path,
                                                               appGuid: appGuid,
                                                               clientIdentifier: clientIdentifier,
                                                               userIpAddress: userIpAddress,
                                                               body: await request.GetBodyAsync(),
                                                               headers: request.Headers.GetHeaders(),
                                                               queryString: request.QueryString.ToString());

        request.Headers.Add(RequestHeaderKeys.SystemRequestId, requestId.ToString());
    }
}

public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}