using Core.Constants;
using Core.Results;
using Ecommerce.Services.Order.Application.Commands;
using Ecommerce.Services.Order.Application.Dtos;
using Ecommerce.Services.Order.Domain.OrderAggregate;
using Ecommerce.Services.Order.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, JsonDataResult<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;
        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<JsonDataResult<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Address(
                request.Address.Province, 
                request.Address.District, 
                request.Address.Street, 
                request.Address.ZipCode, 
                request.Address.Line
                );

            Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(request.CustomerId, newAddress);

            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PhotoUrl);
            });

            await _context.Orders.AddAsync(newOrder);
            var result = await _context.SaveChangesAsync();

            return new SuccessJsonDataResult<CreatedOrderDto>(new CreatedOrderDto {OrderId = newOrder.Id }, Messages.RecordsAdded);
        }
    }
}
