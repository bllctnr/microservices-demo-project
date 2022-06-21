namespace CouponAPI.Entities
{
    [Dapper.Contrib.Extensions.Table("coupon")]
    public class Coupon : IEntity
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int Rate { get; set; }
        public string? Code { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
