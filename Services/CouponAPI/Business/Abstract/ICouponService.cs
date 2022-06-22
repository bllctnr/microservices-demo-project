using Core.Results;
using CouponAPI.Entities.Dtos;

namespace CouponAPI.Business.Abstract
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
