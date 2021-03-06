namespace Ecommerce.Services.Catalog.APIEntities.Dtos
{
    public class ProductCreateDto : IDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedDate { get { return DateTime.Now; } }
        public FeatureDto Feature { get; set; }
    }
}
