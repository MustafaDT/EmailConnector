
using Microsoft.Extensions.Logging;
using JadwaEmailConnector.Application.Dtos;
using JadwaEmailConnector.Application.Entites;
using JadwaEmailConnector.Application.Interfaces.IRepositories;
using JadwaEmailConnector.Application.Interfaces.IServices;

namespace JadwaEmailConnector.Application.Services
{
    public class EmailRequestRepository : IEmailRequestService
    {
        private readonly ILogger<EmailRequestRepository> _logger;
        private readonly IEmailRequestRepository _emailRequestRepository;
        public EmailRequestRepository( ILogger<EmailRequestRepository> logger, IEmailRequestRepository emailRequestRepository)
        {
            _logger = logger;
            _emailRequestRepository = emailRequestRepository;
        }
       
        public async Task<WorkResponse<string>> AddEmailRequestAsync(EmailRequestViewModel emailRequest)
        {
            try
            {
                var model = new EmailRequest()
                {
                   ToName= emailRequest.ToName,
                   ToAddress= emailRequest.ToAddress,
                   EmailFromAddress= emailRequest.EmailFromAddress,
                   EmailFromName= emailRequest.EmailFromName,
                   EmailSmtpHost= emailRequest.EmailSmtpHost,
                   EmailSmtpPort= emailRequest.EmailSmtpPort,
                   EmailAuthenticate= emailRequest.EmailAuthenticate,
                   EmailSubject= emailRequest.EmailSubject,
                   EmailBody= emailRequest.EmailBody,
                   EmailCc= emailRequest.EmailCc,
                   EmailBcc= emailRequest.EmailBcc,
                   EmailType= emailRequest.EmailType,
                   EmailStatus= emailRequest.EmailStatus,
                   IsResend= emailRequest.IsResend,
                   ApplicationId= emailRequest.ApplicationId,
                   CreatedDate= emailRequest.CreatedDate,
                   LastAttemptedDate = emailRequest.LastAttemptedDate
                };

                await _emailRequestRepository.AddEmailRequestAsync(model);
                return WorkResponse<string>.Success("Email Send Successfully");

            }
            catch (Exception e)
            {
                _logger.LogError($"AddEmailRequest Error={e.Message}");
                return WorkResponse<string>.Error(e.Message);
            }
            
        }
    }
}
