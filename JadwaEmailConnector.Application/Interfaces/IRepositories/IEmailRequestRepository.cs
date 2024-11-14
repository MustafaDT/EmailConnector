using JadwaEmailConnector.Application.Entites;

namespace JadwaEmailConnector.Application.Interfaces.IRepositories
{
    public interface IEmailRequestRepository
    {
        Task<int?> AddEmailRequestAsync(EmailRequest emailRequest);
    }
}
