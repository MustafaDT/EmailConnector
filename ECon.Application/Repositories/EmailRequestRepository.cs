
using Dapper;
using System.Data;
using ECon.Application.Entities;
using ECon.Application.Interfaces.IRepositories;

namespace ECon.Application.Repositories
{
    public class EmailRequestRepository : IEmailRequestRepository
    {
        private readonly IDbConnection _dbConnection;
        public EmailRequestRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
       
        public async Task<int> AddEmailRequestAsync(EmailRequest emailRequest)
        {
          
                var query = @"
            INSERT INTO EmailRequest (ToName, ToAddress, EmailFromAddress, EmailFromName, EmailSmtpHost, EmailSmtpPort, 
                                      EmailAuthenticate, EmailPassword, EmailSubject, EmailBody, EmailCc, EmailBcc, EmailType, 
                                      EmailStatus, IsResend, ApplicationId, CreatedDate, LastAttemptedDate)
            VALUES (@ToName, @ToAddress, @EmailFromAddress, @EmailFromName, @EmailSmtpHost, @EmailSmtpPort, 
                    @EmailAuthenticate, @EmailPassword, @EmailSubject, @EmailBody, @EmailCc, @EmailBcc, @EmailType, 
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
                    emailRequest.EmailPassword,
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

        public async Task<EmailRequest?> GetEmailRequestAsync(int emailRequestId)
        {
            var query = "SELECT * FROM EmailRequest WHERE EmailRequestId = @EmailRequestId";
            var parameters = new { EmailRequestId = emailRequestId };

            return await _dbConnection.QueryFirstOrDefaultAsync<EmailRequest>(query, parameters);
        }
        public async Task<int> UpdateEmailRequestAsync(EmailRequest emailRequest)
        {
            var query = @"
        UPDATE EmailRequest
        SET 
            ToName = @ToName,
            ToAddress = @ToAddress,
            EmailFromAddress = @EmailFromAddress,
            EmailFromName = @EmailFromName,
            EmailSmtpHost = @EmailSmtpHost,
            EmailSmtpPort = @EmailSmtpPort,
            EmailAuthenticate = @EmailAuthenticate,
            EmailSubject = @EmailSubject,
            EmailBody = @EmailBody,
            EmailCc = @EmailCc,
            EmailBcc = @EmailBcc,
            EmailType = @EmailType,
            EmailStatus = @EmailStatus,
            IsResend = @IsResend,
            ApplicationId = @ApplicationId,
            LastAttemptedDate = @LastAttemptedDate
        WHERE EmailRequestId = @EmailRequestId";

            var parameters = new
            {
                emailRequest.EmailRequestId,
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
                emailRequest.LastAttemptedDate
            };

            return await _dbConnection.ExecuteAsync(query, parameters);
        }
    }
}
