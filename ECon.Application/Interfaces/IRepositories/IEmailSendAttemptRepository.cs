using ECon.Application.Entities;

namespace ECon.Application.Interfaces.IRepositories
{
    public interface IEmailSendAttemptRepository
    {
        Task<int?> AddEmailSendAttemptAsync(EmailSendAttempt emailRequest);
    }
}
