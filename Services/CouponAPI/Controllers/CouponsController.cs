using Core.IdentityService;
using CouponAPI.Business.Abstract;
using CouponAPI.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly ICouponService _couponService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CouponsController(ICouponService couponService, ISharedIdentityService sharedIdentityService)
        {
            _couponService = couponService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _couponService.GetAll();
            if (!result.Success)
            {
                return BadRequest(Response);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _couponService.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Route("/api/[controller]/[action]/{code}")]
        [HttpGet]
        public async Task<IActionResult> GetByCode(string code) 
        {
            var userId = _sharedIdentityService.GetUserId();
            var result = await _couponService.GetByUserIdAndCode(code, userId);

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CouponDto coupon)
        {
            var result = await _couponService.Add(coupon);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CouponDto coupon)
        {
            var result = await _couponService.Update(coupon);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _couponService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
