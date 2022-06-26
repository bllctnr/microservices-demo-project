using ShoppingCartAPI.Entities.Dto;

namespace ShoppingCartAPI.Dtos
{
    public class ShoppingCartItemDto : IDto
    {
        public int Quantity { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
