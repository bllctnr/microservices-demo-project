using ShoppingCartAPI.Business.Abstract;
using ShoppingCartAPI.Entities.Dto;
using Core.IdentityService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public ShoppingCartsController(IShoppingCartService basketService, ISharedIdentityService sharedIdentityService)
        {
            _shoppingCartService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = _sharedIdentityService.GetUserId();
            var response = await _shoppingCartService.GetShoppingCart(userId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateCart(ShoppingCartDto cart) 
        {
            var userId = _sharedIdentityService.GetUserId();
            cart.UserId = userId;

            var response = await _shoppingCartService.SaveOrUpdate(cart);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCart()
        {
            var userId = _sharedIdentityService.GetUserId();
            var response = await _shoppingCartService.Delete(userId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
