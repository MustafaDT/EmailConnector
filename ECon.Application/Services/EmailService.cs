using ECon.Application.Dtos;
using ECon.Application.Interfaces.IServices;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace ECon.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task<string> SendAsync(EmailServiceParam emailServiceParam)
        {
            try
            {
                var message = new MimeMessage();

                message.From.Add(
                    new MailboxAddress(emailServiceParam.EmailFromName, emailServiceParam.EmailFromAddress));
                message.To.Add(new MailboxAddress(emailServiceParam.ToName, emailServiceParam.ToAddress));

                if (!string.IsNullOrWhiteSpace(emailServiceParam.EmailBcc))
                    message.Bcc.Add(new MailboxAddress(emailServiceParam.EmailBcc, emailServiceParam.EmailBcc));

                message.Subject = emailServiceParam.Subject;
                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = emailServiceParam.Body
                };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(emailServiceParam.EmailSmtpHost, emailServiceParam.EmailSmtpPort);

                if (emailServiceParam.EmailAuthenticate)
                    await smtp.AuthenticateAsync(emailServiceParam.EmailFromAddress, emailServiceParam.EmailPassword);

                var response = await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);

                #region EmailLog will uncommit after include EmailLog

                //var emailLogsObj = new EmailLogs();

                //emailLogsObj.ToName = toName;
                //emailLogsObj.ToAddress = toAddress;
                //emailLogsObj.EmailFrom = _applicationParameters.Value.EmailFromAddress;
                //emailLogsObj.EmailBody = body;
                //emailLogsObj.EmailSubject = subject;
                //emailLogsObj.EmailCc = "";
                //emailLogsObj.EmailBcc = _applicationParameters.Value.EmailBcc;
                //emailLogsObj.CreatedDate = DateTime.Now;
                //_unitOfWork.EmailLogs.Add(emailLogsObj);
                //_unitOfWork.Save();

                #endregion

                _logger.LogInformation($"Email Service Response={response}");
                return response;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
           
        }

    }
}