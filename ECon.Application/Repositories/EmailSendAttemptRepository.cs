
using Dapper;
using System.Data;
using ECon.Application.Entities;
using ECon.Application.Interfaces.IRepositories;

namespace ECon.Application.Repositories
{
    public class EmailSendAttemptRepository : IEmailSendAttemptRepository
    {
        private readonly IDbConnection _dbConnection;
        public EmailSendAttemptRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
       
        public async Task<int?> AddEmailSendAttemptAsync(EmailSendAttempt emailSendAttempt)
        {
          
                var query = @"
            INSERT INTO EmailSendAttempt (EmailRequestId, ExceptionJson, Message, AttemptDate, EmailStaus)
            VALUES (@EmailRequestId, @ExceptionJson, @Message, @AttemptDate, @EmailStaus);
            SELECT CAST(SCOPE_IDENTITY() as int);";

                var parameters = new
                {
                    emailSendAttempt.EmailRequestId,
                    emailSendAttempt.ExceptionJson,
                    emailSendAttempt.Message,
                    emailSendAttempt.AttemptDate,
                    emailSendAttempt.EmailStaus,
                };

                return await _dbConnection.ExecuteScalarAsync<int>(query, parameters);

            
          
            
        }
    }
}
