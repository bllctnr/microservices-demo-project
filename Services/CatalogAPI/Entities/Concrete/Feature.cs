using Ecommerce.Services.Catalog.APIEntities.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ecommerce.Services.Catalog.APIEntities
{
    public class Feature : IEntity
    {
        public int Duration { get; set; }
    }
}
