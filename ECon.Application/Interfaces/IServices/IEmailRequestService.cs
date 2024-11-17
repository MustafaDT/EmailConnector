using ECon.Application.Dtos;
using ECon.Application;

namespace ECon.Application.Interfaces.IServices
{
    public interface IEmailRequestService
    {
        Task<WorkResponse<string>> AddEmailRequestAsync(EmailRequestViewModel emailRequest);
    }
}
