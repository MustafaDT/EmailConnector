using JadwaEmailConnector.Application;
using JadwaEmailConnector.Application.Dtos;
using JadwaEmailConnector.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace JadwaEmailConnector.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailServiceController : ControllerBase
    {
        public IEmailRequestService _emailRequestService;

        public EmailServiceController(IEmailRequestService emailRequestService)
        {
            _emailRequestService = emailRequestService;
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail(EmailRequestViewModel model)
        {

           var response =await  _emailRequestService.AddEmailRequestAsync(model);
           if (!response.Status)
               return UnprocessableEntity(response);

           return Ok(response);
        }
    }
}
