using CatalogAPI.Entities.Dtos;
using Core.Results;

namespace CatalogAPI.Business.Abstract
{
    internal interface ICourseService
    {
        Task<IJsonDataResult<List<CourseDto>>> GetAllAsync();
        Task<IJsonDataResult<CourseDto>> GetByIdAsync(string id);
        Task<IJsonDataResult<List<CourseDto>>> GetAllByUserIdAsync(string userId);
        Task<IJsonDataResult<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
        Task<JsonResult> UpdateAsync(CourseUpdateDto courseUpdateDto);
        Task<JsonResult> DeleteAsync(string Id);
    }
}
