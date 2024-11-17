using JadwaEmailConnector.Application.Dtos;
using JadwaEmailConnector.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace JadwaEmailConnector.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailServiceController : ControllerBase
    {
        public IEmailRequestService EmailRequestService;

        public EmailServiceController(IEmailRequestService emailRequestService)
        {
            EmailRequestService = emailRequestService;
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail(EmailRequestViewModel model)
        {

           var response =await  EmailRequestService.AddEmailRequestAsync(model);
           if (!response.Status)
               return UnprocessableEntity(response);

           return Ok(response);
        }
    }
}
