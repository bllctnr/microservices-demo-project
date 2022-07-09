using Core.Results;
using Ecommerce.Services.Payment.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Services.Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult RecievePayment(PaymentDto paymentDto) 
        {
            return Ok(new SuccessJsonResult("Payment Recieved"));
        }
    }
}
