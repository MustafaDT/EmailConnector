using JadwaEmailConnector.Application.Dtos;
using JadwaEmailConnector.Application.Entites;

namespace JadwaEmailConnector.Application.Interfaces.IServices
{
    public interface IEmailRequestService
    {
        Task<WorkResponse<string>> AddEmailRequestAsync(EmailRequestViewModel emailRequest);
    }
}
