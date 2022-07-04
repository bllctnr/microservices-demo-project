using AutoMapper;
using Ecommerce.Services.CouponCode.APIEntities.Dtos;

namespace Ecommerce.Services.CouponCode.APIEntities.Mappings
{
    public class GeneralMappings : Profile
    {
        public GeneralMappings() 
        {
            CreateMap<Coupon, CouponDto>().ReverseMap();
        }
        
    }
}
