namespace Ecommerce.Services.CouponCode.APIEntities.Dtos
{
    public class CouponDto : IDto
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int Rate { get; set; }
        public string? Code { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
