using ECon.Application.Entities;

namespace ECon.Application.Interfaces.IRepositories
{
    public interface IEmailRequestRepository
    {
        Task<int> AddEmailRequestAsync(EmailRequest emailRequest);
        Task<EmailRequest?> GetEmailRequestAsync(int emailRequestId);
        Task<int> UpdateEmailRequestAsync(EmailRequest emailRequest);
    }
}
