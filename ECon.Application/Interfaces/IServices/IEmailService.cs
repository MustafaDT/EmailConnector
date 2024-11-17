using ECon.Application.Dtos;

namespace ECon.Application.Interfaces.IServices
{
    public interface IEmailService
    {
        Task<string> SendAsync(EmailServiceParam emailServiceParam);
    }
}
