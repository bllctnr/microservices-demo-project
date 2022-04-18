using AutoMapper;
using CatalogAPI.Entities;
using CatalogAPI.Entities.Dtos;

namespace CatalogAPI.Mapping
{
    public class GeneralMappings : Profile
    {
        public GeneralMappings()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

            CreateMap<Course, CourseCreateDto>().ReverseMap();
            CreateMap<Course, CourseUpdateDto>().ReverseMap();
        }
    }
}
