using CatalogAPI.Entities.Dtos;
using Core.Results;

namespace CatalogAPI.Business.Abstract
{
    public interface ICourseService
    {
        Task<IJsonDataResult<List<CourseDto>>> GetAllAsync();
        Task<IJsonDataResult<CourseDto>> GetByIdAsync(string id);
        Task<IJsonDataResult<List<CourseDto>>> GetAllByUserIdAsync(string userId);
        Task<IJsonDataResult<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
        Task<IJsonResult> UpdateAsync(CourseUpdateDto courseUpdateDto);
        Task<IJsonResult> DeleteAsync(string Id);
    }
}
