using JadwaEmailConnector.Application.Dtos;

namespace JadwaEmailConnector.Application.Interfaces.IServices
{
    public interface IEmailRequestService
    {
        Task<WorkResponse<string>> AddEmailRequestAsync(EmailRequestViewModel emailRequest);
    }
}
