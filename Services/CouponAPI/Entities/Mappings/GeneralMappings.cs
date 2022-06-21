using AutoMapper;
using CouponAPI.Entities.Dtos;

namespace CouponAPI.Entities.Mappings
{
    public class GeneralMappings : Profile
    {
        public GeneralMappings() 
        {
            CreateMap<Coupon, CouponDto>().ReverseMap();
        }
        
    }
}
