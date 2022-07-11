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
        public OrderItem()
        {

        }

        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string Picture { get; private set; }
        public Decimal Price { get; private set; }

        public OrderItem(string productId, string productName, string photoUrl, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            Picture = photoUrl;
            Price = price;
        }

        public void UpdateOrderItem(string productName, decimal price) 
        {
            ProductName = productName;
            Price = price;
        }
    }
}
