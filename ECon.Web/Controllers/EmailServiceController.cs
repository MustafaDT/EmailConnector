using ECon.Application.Dtos;
using ECon.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ECon.Web.Controllers
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
