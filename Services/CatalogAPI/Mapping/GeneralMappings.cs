using AutoMapper;
using Ecommerce.Services.Catalog.APIEntities;
using Ecommerce.Services.Catalog.APIEntities.Dtos;

namespace Ecommerce.Services.Catalog.APIMapping
{
    public class GeneralMappings : Profile
    {
        public GeneralMappings()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
        }
    }
}
