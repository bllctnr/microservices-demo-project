using Ecommerce.Services.Catalog.APIEntities;
using Ecommerce.Services.Catalog.APIEntities.Dtos;
using Core.Results;

namespace Ecommerce.Services.Catalog.APIBusiness.Abstract
{
    public interface ICategoryService
    {
        Task<IJsonDataResult<CategoryDto>> GetByIdAsync(string categoryId);
        Task<IJsonDataResult<List<CategoryDto>>> GetAllAsync();
        Task<IJsonDataResult<CategoryDto>> CreateAsync(CategoryDto categoryDto);
        Task<IJsonResult> DeleteAsync(string categoryId);
    }
}
