
using System.Data;
using Dapper;
using ECon.Application.Interfaces.IRepositories;


namespace ECon.Application.Repositories
{
    public class LoggingRepository : ILoggingRepository
    {
        private readonly IDbConnection _dbConnection;
        public LoggingRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> InsertSystemRequest(string? action, string? appGuid, string? clientIdentifier, string? userIpAddress, string? body, string? headers, string? queryString)
        {

            var query = @"
            INSERT INTO [dbo].[SystemRequests] (
                [Action],
                [AppGuid],
                [ClientIdentifier],
                [UserIpAddress],
                [Body],
                [Headers],
                [QueryString]
            ) VALUES (
                @Action,
                @AppGuid,
                @ClientIdentifier,
                @UserIpAddress,
                @Body,
                @Headers,
                @QueryString
            );

            SELECT CAST(SCOPE_IDENTITY() as int);
        ";

         

            var requestId = await _dbConnection.QuerySingleAsync<int>(query, new
            {
                Action = action,
                AppGuid = appGuid,
                ClientIdentifier = clientIdentifier,
                UserIpAddress = userIpAddress,
                Body = body,
                Headers = headers,
                QueryString = queryString
            });

            return requestId;
        }


        public async Task<int> InsertSystemResponse(int? systemRequestId, int? statusCode, string? body, string? headers)
        {
            var query = @"
            INSERT INTO [dbo].[SystemResponses] (
                [SystemRequestId],
                [StatusCode],
                [Body],
                [Headers]
            ) VALUES (
                @SystemRequestId,
                @StatusCode,
                @Body,
                @Headers
            );

            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
            var responseId = await _dbConnection.QuerySingleAsync<int>(query, new
            {
                SystemRequestId = systemRequestId,
                StatusCode = statusCode,
                Body = body,
                Headers = headers
            });

            return responseId;
        }

        public async Task<int> InsertSystemException(string? appGuid, string? exceptionType, string? message, string? stackTrace, bool? handled, string? rawException, int? systemRequestId)
        {
          var query = @"
            INSERT INTO [dbo].[SystemExceptions] (
                [SystemRequestId],
                [AppGuid], 
                [ExceptionType], 
                [Message], 
                [StackTrace], 
                [Handled], 
                [RawException]
            ) VALUES (
                @SystemRequestId,
                @AppGuid,
                @ExceptionType,
                @Message,
                @StackTrace,
                @Handled,
                @RawException
            );

            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
            
            try
            {
                await _dbConnection.QuerySingleAsync<int>(query, new
                {
                    SystemRequestId = systemRequestId,
                    AppGuid = appGuid,
                    ExceptionType = exceptionType,
                    Message = message,
                    StackTrace = stackTrace,
                    Handled = handled,
                    RawException = rawException
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return 0;
        }


    }
}
