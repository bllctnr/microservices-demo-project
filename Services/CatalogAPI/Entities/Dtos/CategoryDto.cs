using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CatalogAPI.Entities.Dtos
{
    public class CategoryDto : IDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
