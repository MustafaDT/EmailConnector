
using Microsoft.Extensions.Logging;
using JadwaEmailConnector.Application.Dtos;
using JadwaEmailConnector.Application.Entites;
using JadwaEmailConnector.Application.Interfaces.IRepositories;
using JadwaEmailConnector.Application.Interfaces.IServices;

namespace JadwaEmailConnector.Application.Services
{
    public class EmailRequestService : IEmailRequestService
    {
        private readonly ILogger<EmailRequestService> _logger;
        private readonly IEmailRequestRepository _emailRequestRepository;
        private readonly IEmailSendAttemptRepository _emailSendAttemptRepository;
        private readonly IEmailService _emailService;
        public EmailRequestService( ILogger<EmailRequestService> logger, IEmailRequestRepository emailRequestRepository, IEmailService emailService, IEmailSendAttemptRepository emailSendAttemptRepository)
        {
            _logger = logger;
            _emailRequestRepository = emailRequestRepository;
            _emailService = emailService;
            _emailSendAttemptRepository = emailSendAttemptRepository;
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
                   EmailPassword = emailRequest.EmailPassword,
                   EmailSubject= emailRequest.EmailSubject,
                   EmailBody= emailRequest.EmailBody,
                   EmailCc= emailRequest.EmailCc,
                   EmailBcc= emailRequest.EmailBcc,
                   EmailType= emailRequest.IsUrgent ?'U':'N',
                   EmailStatus= 'Q',
                   IsResend= emailRequest.IsResend,
                   ApplicationId= emailRequest.ApplicationId,
                   CreatedDate= DateTime.Now,
                };

               var emailRequestId =  await _emailRequestRepository.AddEmailRequestAsync(model);
               if (emailRequest.IsUrgent)
                {
                    var emailSendAttempt = new EmailSendAttempt()
                    {
                        EmailRequestId = emailRequestId,
                        AttemptDate = DateTime.Now,
                    };
                    var emailServiceParam = new EmailServiceParam
                    {
                        ToName = emailRequest.ToName,
                        ToAddress = emailRequest.ToAddress,
                        Subject = emailRequest.EmailSubject,
                        Body = emailRequest.EmailBody,
                        EmailSmtpHost = emailRequest.EmailSmtpHost,
                        EmailSmtpPort = emailRequest.EmailSmtpPort,
                        EmailFromAddress = emailRequest.EmailFromAddress,
                        EmailPassword = emailRequest.EmailPassword,
                        EmailFromName = emailRequest.EmailFromName,
                        EmailBcc = emailRequest.EmailBcc,
                        EmailAuthenticate = emailRequest.EmailAuthenticate
                    };

                    var response = await _emailService.SendAsync(emailServiceParam);
                    var emailRequestUpdate = await _emailRequestRepository.GetEmailRequestAsync(emailRequestId);
                    if (emailRequestUpdate != null)
                    {
                        if (response.StartsWith("2.0.0", StringComparison.InvariantCultureIgnoreCase))
                        {
                            emailSendAttempt.Message = response;
                            emailSendAttempt.EmailStaus = 'S';
                            emailRequestUpdate.EmailStatus = 'S';
                            emailRequestUpdate.LastAttemptedDate = DateTime.Now;
                        }
                        else
                        {
                            emailSendAttempt.ExceptionJson = response;
                            emailSendAttempt.EmailStaus = 'F';
                            emailRequestUpdate.EmailStatus = 'F';
                            emailRequestUpdate.LastAttemptedDate = DateTime.Now;
                            emailRequestUpdate.IsResend = true;
                        }

                        await _emailRequestRepository.UpdateEmailRequestAsync(emailRequestUpdate);
                        await _emailSendAttemptRepository.AddEmailSendAttemptAsync(emailSendAttempt);
                    }
                    
                }
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
