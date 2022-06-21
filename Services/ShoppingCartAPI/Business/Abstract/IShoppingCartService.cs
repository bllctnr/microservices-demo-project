using ShoppingCartAPI.Entities.Dto;
using Core.Results;

namespace ShoppingCartAPI.Business.Abstract
{
    public interface IShoppingCartService
    {
        Task<IJsonResult> GetShoppingCart(string userId);
        Task<IJsonResult> SaveOrUpdate(ShoppingCartDto cart);
        Task<IJsonResult> Delete(string userId);
    }
}
