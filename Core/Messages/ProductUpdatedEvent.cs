using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Messages
{
    public class ProductUpdatedEvent
    {
        public string Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
