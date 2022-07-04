using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Order.Application.Dtos
{
    public class OrderItemDto
    {
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string PhotoUrl { get; private set; }
        public Decimal Price { get; private set; }
    }
}
