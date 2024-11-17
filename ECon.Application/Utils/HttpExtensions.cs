using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace ECon.Application.Utils;

public static class HttpExtensions
{
    public static async Task<string> GetBodyAsync(this HttpRequest httpRequest)
    {
        httpRequest.EnableBuffering();
        var requestBodyStream = new MemoryStream();
        await httpRequest.Body.CopyToAsync(requestBodyStream);
        requestBodyStream.Seek(0, SeekOrigin.Begin);

        string body;

        using (var reader = new StreamReader(requestBodyStream))
        {
            body = await reader.ReadToEndAsync();
        }

        httpRequest.Body.Seek(0, SeekOrigin.Begin);

        return body;
    }

    public static string GetHeaders(this IHeaderDictionary headers)
    {
        var headersString = string.Join(Environment.NewLine, headers.Select(header => $"{header.Key}: {header.Value}"));
        return headersString;
    }

    public static string GetHeaders(this HttpHeaders headers)
    {
        var headersString = string.Join(Environment.NewLine, headers.Select(header => $"{header.Key}: {header.Value}"));
        return headersString;
    }
}