using Ecommerce.Services.ShoppingCart.API.Dtos;

namespace Ecommerce.Services.ShoppingCart.API.Entities.Dto
{
    public class ShoppingCartDto : IDto
    {
        public string? UserId { get; set; }
        public string DiscountCode { get; set; }
        public List<ShoppingCartItemDto> basketItems { get; set; }

        public decimal TotalPrice 
        {
            get => basketItems.Sum(bi => bi.Price * bi.Quantity);
        }
    }
}
