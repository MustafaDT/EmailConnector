using JadwaEmailConnector.Application.Entites;

namespace JadwaEmailConnector.Application.Interfaces.IRepositories
{
    public interface IEmailSendAttemptRepository
    {
        Task<int?> AddEmailSendAttemptAsync(EmailSendAttempt emailRequest);
    }
}
