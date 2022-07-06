using Core.Results;
using Ecommerce.Services.Order.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<JsonDataResult<CreatedOrderDto>>
    {
        public string CustomerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public AddressDto Address { get; set; }
    }
}
