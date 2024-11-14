
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;
using JadwaEmailConnector.Application.Interfaces.IRepositories;
using JadwaEmailConnector.Application.Entites;

namespace JadwaEmailConnector.Application.Repositories
{
    public class EmailRequestRepository : IEmailRequestRepository
    {
        private readonly IDbConnection _dbConnection;
        public EmailRequestRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
       
        public async Task<int?> AddEmailRequestAsync(EmailRequest emailRequest)
        {
          
                var query = @"
            INSERT INTO EmailRequest (ToName, ToAddress, EmailFromAddress, EmailFromName, EmailSmtpHost, EmailSmtpPort, 
                                      EmailAuthenticate, EmailSubject, EmailBody, EmailCc, EmailBcc, EmailType, 
                                      EmailStatus, IsResend, ApplicationId, CreatedDate, LastAttemptedDate)
            VALUES (@ToName, @ToAddress, @EmailFromAddress, @EmailFromName, @EmailSmtpHost, @EmailSmtpPort, 
                    @EmailAuthenticate, @EmailSubject, @EmailBody, @EmailCc, @EmailBcc, @EmailType, 
                    @EmailStatus, @IsResend, @ApplicationId, @CreatedDate, @LastAttemptedDate);
            SELECT CAST(SCOPE_IDENTITY() as int);";

                var parameters = new
                {
                    emailRequest.ToName,
                    emailRequest.ToAddress,
                    emailRequest.EmailFromAddress,
                    emailRequest.EmailFromName,
                    emailRequest.EmailSmtpHost,
                    emailRequest.EmailSmtpPort,
                    emailRequest.EmailAuthenticate,
                    emailRequest.EmailSubject,
                    emailRequest.EmailBody,
                    emailRequest.EmailCc,
                    emailRequest.EmailBcc,
                    emailRequest.EmailType,
                    emailRequest.EmailStatus,
                    emailRequest.IsResend,
                    emailRequest.ApplicationId,
                    emailRequest.CreatedDate,
                    emailRequest.LastAttemptedDate
                };

                return await _dbConnection.ExecuteScalarAsync<int>(query, parameters);

            
          
            
        }
    }
}
