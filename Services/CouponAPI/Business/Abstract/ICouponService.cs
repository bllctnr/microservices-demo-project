using Core.Results;
using Ecommerce.Services.CouponCode.APIEntities.Dtos;

namespace Ecommerce.Services.CouponCode.APIBusiness.Abstract
{
    public interface ICouponService
    {
        Task<IJsonDataResult<List<CouponDto>>> GetAll();
        Task<IJsonDataResult<CouponDto>> GetById(int id);
        Task<IJsonResult> Add(CouponDto coupon);
        Task<IJsonResult> Update(CouponDto coupon);
        Task<IJsonResult> Delete(int id);
        Task<IJsonDataResult<CouponDto>> GetByUserIdAndCode(string code, string userId);
    }
}
