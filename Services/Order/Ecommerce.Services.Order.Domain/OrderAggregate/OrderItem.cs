using Ecommerce.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Order.Domain.OrderAggregate
{
    public class OrderItem : Entity
    {
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string PhotoUrl { get; private set; }
        public Decimal Price { get; private set; }

        public OrderItem(string productId, string productName, string photoUrl, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            PhotoUrl = photoUrl;
            Price = price;
        }

        public void UpdateOrderItem(string productName, string photoUrl, decimal price) 
        {
            ProductName = productName;
            PhotoUrl = photoUrl;
            Price = price;
        }
    }
}
