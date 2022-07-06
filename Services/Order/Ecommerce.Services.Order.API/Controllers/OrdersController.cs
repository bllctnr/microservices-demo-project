using Core.IdentityService;
using Ecommerce.Services.Order.Application.Commands;
using Ecommerce.Services.Order.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Services.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrdersController(IMediator metiator, ISharedIdentityService sharedIdentityService)
        {
            _mediator = metiator;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders() 
        {
            var response = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = _sharedIdentityService.GetUserId()});
            return Ok(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(CreateOrderCommand createOrderCommand) 
        {
            var response = await _mediator.Send(createOrderCommand);
            return Ok(response.Data);
        }
    }
}
