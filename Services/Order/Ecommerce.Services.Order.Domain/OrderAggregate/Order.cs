using Ecommerce.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Order.Domain.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public DateTime CreatedDate { get; private set; }

        public Address Address { get; private set; }

        public string CustomerId { get; private set; }

        private readonly List<OrderItem> _orderItems;

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order()
        {
        }

        public Order(string customerId, Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            CustomerId = customerId;
            Address = address;
        }

        public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);

            if (!existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);

                _orderItems.Add(newOrderItem);
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
    }

    // EF Core features
    // - Owned Types
    // - Shadow Property
    // - Backing Field
}
