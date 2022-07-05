using Core.Results;
using Ecommerce.Services.Order.Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Order.Application.Queries
{
    class GetOrdersByUserIdQuery : IRequest<IJsonDataResult<List<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}
