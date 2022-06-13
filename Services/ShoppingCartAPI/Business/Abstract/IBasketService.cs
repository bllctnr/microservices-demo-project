using BasketAPI.Entities.Dto;
using Core.Results;

namespace BasketAPI.Business.Abstract
{
    public interface IBasketService
    {
        Task<IJsonDataResult<BasketDto>> GetBasket(string userId);
        Task<IJsonDataResult<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<IJsonDataResult<bool>> Delete(string userId);
    }
}
