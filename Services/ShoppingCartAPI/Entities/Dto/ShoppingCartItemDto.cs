using Ecommerce.Services.ShoppingCart.API.Entities.Dto;

namespace Ecommerce.Services.ShoppingCart.API.Dtos
{
    public class ShoppingCartItemDto : IDto
    {
        public int Quantity { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
