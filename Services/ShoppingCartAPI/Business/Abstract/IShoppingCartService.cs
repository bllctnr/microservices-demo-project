using Ecommerce.Services.ShoppingCart.API.Entities.Dto;
using Core.Results;

namespace Ecommerce.Services.ShoppingCart.API.Business.Abstract
{
    public interface IShoppingCartService
    {
        Task<IJsonResult> GetShoppingCart(string userId);
        Task<IJsonResult> SaveOrUpdate(ShoppingCartDto cart);
        Task<IJsonResult> Delete(string userId);
    }
}
