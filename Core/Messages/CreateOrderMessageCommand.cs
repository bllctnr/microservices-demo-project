using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Messages
{
    public class CreateOrderMessageCommand
    {
        public string CustomerId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Address Address { get; set; }

    }

    public class OrderItem 
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string PhotoUrl { get; set; }
        public Decimal Price { get; set; }
    }

    public class Address 
    {
        public string Province { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string Line { get; set; }
    }
}
