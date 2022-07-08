using Core.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Services.Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult RecievePayment() 
        {
            return Ok(new SuccessJsonResult("Payment Recieved"));
        }
    }
}
