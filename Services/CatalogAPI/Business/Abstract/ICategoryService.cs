using CatalogAPI.Entities;
using CatalogAPI.Entities.Dtos;
using Core.Results;

namespace CatalogAPI.Business.Abstract
{
    public interface ICategoryService
    {
        Task<IJsonDataResult<CategoryDto>> GetByIdAsync(string categoryId);
        Task<IJsonDataResult<List<CategoryDto>>> GetAllAsync();
        Task<IJsonDataResult<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task<IJsonResult> DeleteAsync(string categoryId);
    }
}
