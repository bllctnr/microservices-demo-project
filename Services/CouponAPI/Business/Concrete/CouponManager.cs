using AutoMapper;
using Core.Constants;
using Core.Results;
using CouponAPI.Business.Abstract;
using CouponAPI.Entities;
using CouponAPI.Entities.Dtos;
using Dapper;
using Npgsql;
using System.Data;

namespace CouponAPI.Business.Concrete
{
    public class CouponManager : ICouponService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;

        public CouponManager(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
            _mapper = mapper;
        }

        public async Task<IJsonResult> Add(CouponDto coupon)
        {
            var response = await _dbConnection.ExecuteAsync("INSERT INTO coupon (userid, rate, code) VALUES (@UserId, @Rate, @Code)", _mapper.Map<Coupon>(coupon));
            if (response == 0)
            {
                return new ErrorJsonResult(Messages.RecordCouldNotUpdatedOrAdded);
            }
            return new SuccessJsonResult(Messages.RecordsAdded);
        }

        public async Task<IJsonResult> Delete(int id)
        {
            await _dbConnection.ExecuteAsync("delete from coupon where id=@Id", new { Id = id });
            return new SuccessJsonResult(Messages.RecordsDeleted);
        }

        public async Task<IJsonDataResult<List<CouponDto>>> GetAll()
        {
            var coupons = await _dbConnection.QueryAsync<Coupon>("select * from coupon");
            return new SuccessJsonDataResult<List<CouponDto>>(_mapper.Map<List<CouponDto>>(coupons), Messages.RecordsListed);
        }

        public async Task<IJsonDataResult<CouponDto>> GetById(int id)
        {
            var coupon = await _dbConnection.QueryAsync<Coupon>("select * from coupon where id = @Id", new { Id = id });
            return new SuccessJsonDataResult<CouponDto>(_mapper.Map<CouponDto>(coupon.SingleOrDefault()), Messages.RecordGetted);
        }

        public async Task<IJsonDataResult<CouponDto>> GetByUserIdAndCode(string code, string userId)
        {
            var coupon = await _dbConnection.QueryAsync<Coupon>("select * from coupon where userid=@UserId and code=@Code", new { UserId = userId, Code = code });
            return new SuccessJsonDataResult<CouponDto>(_mapper.Map<CouponDto>(coupon), Messages.RecordGetted);
        }

        public async Task<IJsonResult> Update(CouponDto coupon)
        {
            var response = await _dbConnection.ExecuteAsync("update coupon set userid=@UserId, code=@Code, rate=@Rate where id=@Id", coupon);
            if (response == 0) 
            {
                return new ErrorJsonResult(Messages.RecordCouldNotUpdatedOrAdded);
            }
            return new SuccessJsonResult(Messages.RecordUpdatedOrAdded);
        }
    }
}
