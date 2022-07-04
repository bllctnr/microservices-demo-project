using Ecommerce.Services.ShoppingCart.API.Entities.Dto;
using Core.Constants;
using Core.Results;
using Ecommerce.Services.ShoppingCart.API.DataAccess;
using System.Text.Json;
using Ecommerce.Services.ShoppingCart.API.Business.Abstract;

namespace Ecommerce.Services.ShoppingCart.API.Business.Concrete
{
    public class ShoppingCartManager : IShoppingCartService
    {
        private readonly RedisService _redisService;
        public ShoppingCartManager(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<IJsonResult> Delete(string userId)
        {
            var result = await _redisService.GetDb().KeyDeleteAsync(userId);
            if (result)
            {
                return new ErrorJsonResult(Messages.RecordNotFount);
            }
            return new SuccessJsonResult(Messages.RecordsDeleted);
        }

        public async Task<IJsonResult> GetShoppingCart(string userId)
        {
            var result = await _redisService.GetDb().StringGetAsync(userId);
            if (String.IsNullOrEmpty(result)) 
            {
                return new ErrorJsonResult(Messages.RecordNotFount);
            }

            return new SuccessJsonDataResult<ShoppingCartDto>(JsonSerializer.Deserialize<ShoppingCartDto>(result), Messages.RecordsListed);
        }

        public async Task<IJsonResult> SaveOrUpdate(ShoppingCartDto cart)
        {
            var status = await _redisService.GetDb().StringSetAsync(cart.UserId, JsonSerializer.Serialize(cart));
            if (status != true)
            {
                return new ErrorJsonResult(Messages.RecordUpdatedOrAdded);
            }
            return new SuccessJsonResult(Messages.RecordUpdatedOrAdded);
        }
    }
}
