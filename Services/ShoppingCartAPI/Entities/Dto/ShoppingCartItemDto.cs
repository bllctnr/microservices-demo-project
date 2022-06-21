using ShoppingCartAPI.Entities.Dto;

namespace ShoppingCartAPI.Dtos
{
    public class ShoppingCartItemDto : IDto
    {
        public int Quantity { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal Price { get; set; }
    }
}
