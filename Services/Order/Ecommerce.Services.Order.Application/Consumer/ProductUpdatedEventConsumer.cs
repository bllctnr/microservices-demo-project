using Core.Messages;
using Ecommerce.Services.Order.Infrastructure;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Order.Application.Consumer
{
    public class ProductUpdatedEventConsumer : IConsumer<ProductUpdatedEvent>
    {
        private readonly OrderDbContext _orderDbContext;
        public ProductUpdatedEventConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task Consume(ConsumeContext<ProductUpdatedEvent> context)
        {
            var orderItems = await _orderDbContext.OrderItems.Where(x => x.ProductId == context.Message.Id).ToListAsync();
            orderItems.ForEach(x =>
            {
                x.UpdateOrderItem(context.Message.ProductName, context.Message.Price);
            });

            await _orderDbContext.SaveChangesAsync();
        }
    }
}
