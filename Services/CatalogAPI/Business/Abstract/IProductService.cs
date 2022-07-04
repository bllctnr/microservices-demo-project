using Ecommerce.Services.Catalog.APIEntities.Dtos;
using Core.Results;

namespace Ecommerce.Services.Catalog.APIBusiness.Abstract
{
    public interface IProductService
    {
        Task<IJsonDataResult<List<ProductDto>>> GetAllAsync();
        Task<IJsonDataResult<ProductDto>> GetByIdAsync(string id);
        Task<IJsonDataResult<List<ProductDto>>> GetAllByUserIdAsync(string userId);
        Task<IJsonDataResult<ProductDto>> CreateAsync(ProductCreateDto productCreateDto);
        Task<IJsonResult> UpdateAsync(ProductUpdateDto productUpdateDto);
        Task<IJsonResult> DeleteAsync(string Id);
    }
}
