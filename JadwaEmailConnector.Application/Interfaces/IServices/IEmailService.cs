using JadwaEmailConnector.Application.Dtos;

namespace JadwaEmailConnector.Application.Interfaces.IServices
{
    public interface IEmailService
    {
        Task<string> SendAsync(EmailServiceParam emailServiceParam);
    }
}
