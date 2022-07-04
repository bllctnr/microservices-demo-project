using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ecommerce.Services.Catalog.APIEntities.Dtos
{
    public class CategoryDto : IDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
