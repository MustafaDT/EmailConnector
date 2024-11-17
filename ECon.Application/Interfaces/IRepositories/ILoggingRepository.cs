
namespace ECon.Application.Interfaces.IRepositories
{
    public interface ILoggingRepository
    {
        Task<int> InsertSystemRequest(string? action, string? appGuid, string? clientIdentifier, string? userIpAddress, string? body, string? headers, string? queryString);
        Task<int> InsertSystemResponse(int? systemRequestId, int? statusCode, string? body, string? headers);
        Task<int> InsertSystemException(string? appGuid, string? exceptionType, string? message, string? stackTrace, bool? handled, string? rawException, int? systemRequestId);
     
    }
}
