using Core.Messages;
using Core.Results;
using Ecommerce.Services.Payment.API.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Services.Payment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public PaymentsController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }
            
        [HttpPost]
        public async Task<IActionResult> RecievePayment(PaymentDto paymentDto) 
        {
            // Rabbitmq
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

            var createOrderMessageCommand = new CreateOrderMessageCommand();
            createOrderMessageCommand.Address.Province = paymentDto.Order.Address.Province;
            createOrderMessageCommand.Address.District = paymentDto.Order.Address.District;
            createOrderMessageCommand.Address.Street = paymentDto.Order.Address.Street;
            createOrderMessageCommand.Address.ZipCode = paymentDto.Order.Address.ZipCode;
            createOrderMessageCommand.Address.Line = paymentDto.Order.Address.Line;

            foreach (var orderItem in paymentDto.Order.OrderItems)
            {
                createOrderMessageCommand.OrderItems.Add(
                    new OrderItem {
                        ProductName = orderItem.ProductName,
                        ProductId = orderItem.ProductId,
                        Price = orderItem.Price,
                        PhotoUrl = orderItem.PhotoUrl
                    });
            }

            await sendEndpoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);
            return Ok(new SuccessJsonResult("Payment Recieved"));
        }
    }
}
