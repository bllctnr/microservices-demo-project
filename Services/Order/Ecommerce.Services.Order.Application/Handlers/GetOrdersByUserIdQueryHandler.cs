using AutoMapper.Internal.Mappers;
using Core.Constants;
using Core.Results;
using Ecommerce.Services.Order.Application.Dtos;
using Ecommerce.Services.Order.Application.Mapping;
using Ecommerce.Services.Order.Application.Queries;
using Ecommerce.Services.Order.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Order.Application.Handlers
{
    class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, IJsonDataResult<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;
        public GetOrdersByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }
        public async Task<IJsonDataResult<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Include(x => x.OrderItems).Where(x => x.CustomerId == request.UserId).ToListAsync();
            if (!orders.Any())
            {
                return new SuccessJsonDataResult<List<OrderDto>>(new List<OrderDto>(), Messages.RecordNotFount);
            }

            var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);
            return  new SuccessJsonDataResult<List<OrderDto>>(ordersDto, Messages.RecordsListed);
        }
    }
}
