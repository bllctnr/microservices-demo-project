using BasketAPI.Dtos;

namespace BasketAPI.Entities.Dto
{
    public class BasketDto : IDto
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set; }
        public List<BasketItemDto> basketItems { get; set; }

        public decimal TotalPrice 
        {
            get => basketItems.Sum(bi => bi.Price * bi.Quantity);
        }
    }
}
