using CatalogAPI.Entities.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CatalogAPI.Entities
{
    public class Feature : IEntity
    {
        public int Duration { get; set; }
    }
}
