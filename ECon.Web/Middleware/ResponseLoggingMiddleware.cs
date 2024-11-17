using ECon.Application.Interfaces.IRepositories;
using ECon.Application.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ECon.Application.Middleware;

public class ResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public ResponseLoggingMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task Invoke(HttpContext context, ILoggingRepository loggingRepository)
    {
        var writeOnlyBody = context.Response.Body;

        string bodyString;

        try
        {
            using var readableWritableBody = new MemoryStream();
            context.Response.Body = readableWritableBody;

            await _next(context);

            readableWritableBody.Position = 0;
            bodyString = await new StreamReader(readableWritableBody).ReadToEndAsync();

            readableWritableBody.Position = 0;
            await readableWritableBody.CopyToAsync(writeOnlyBody);
        }
        finally
        {
            context.Response.Body = writeOnlyBody;
        }

        context.Request.Headers.TryGetValue(RequestHeaderKeys.SystemRequestId, out var requestIdStr);

        int.TryParse(requestIdStr, out var requestId);

        await loggingRepository.InsertSystemResponse(requestId, context.Response.StatusCode, bodyString, context.Response.Headers.GetHeaders());
    }
}

public static class ResponseLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseResponseLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ResponseLoggingMiddleware>();
    }
}