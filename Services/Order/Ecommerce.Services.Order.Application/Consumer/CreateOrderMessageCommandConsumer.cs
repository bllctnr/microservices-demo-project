using Core.Messages;
using Ecommerce.Services.Order.Infrastructure;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Order.Application.Consumer
{
    public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
    {
        // Could be repository pattern
        private readonly OrderDbContext _orderDbContext;
        public CreateOrderMessageCommandConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }
        public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
        {
            var newAddress = new Domain.OrderAggregate.Address(
                context.Message.Address.Province,
                context.Message.Address.District,
                context.Message.Address.Street,
                context.Message.Address.ZipCode,
                context.Message.Address.Line
            );

            Domain.OrderAggregate.Order order = new Domain.OrderAggregate.Order(
                context.Message.CustomerId,
                newAddress
            );

            context.Message.OrderItems.ForEach(item =>
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.Price, item.Picture);
            });


            await _orderDbContext.Orders.AddAsync(order);
            await _orderDbContext.SaveChangesAsync();
        }
    }
}
