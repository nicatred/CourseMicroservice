using Dapper;
using FreeCourse.Services.Discount.Models;
using FreeCourse.Shared.Dtos;
using Npgsql;
using System.Data;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await _dbConnection.ExecuteAsync("delete from discount where id=@id", new { id });
            return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("discount not found", 404);
        }

        public async Task<Response<List<DiscountModel>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<DiscountModel>("Select * From discount");
            return Response<List<DiscountModel>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<DiscountModel>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = await _dbConnection.QueryAsync<DiscountModel>("select * from discount where userid=@userId and code=@code", new { userId = userId, code = code });
            var hasDiscount = discount.FirstOrDefault();
            if (hasDiscount == null)
            {
                return Response<DiscountModel>.Fail("discount not found", 404);
            }
            return Response<DiscountModel>.Success(hasDiscount, 200);
        }

        public async Task<Response<DiscountModel>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<DiscountModel>("select * From Discount where id =@id", new { id })).SingleOrDefault();
            if (discount == null)
            {
                return Response<DiscountModel>.Fail("Discount not found", 404);
            }
            return Response<DiscountModel>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(DiscountModel model)
        {
            try
            {
                var saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount(userid,rate,code) VALUES(@UserId,@Rate,@Code)", model);
                if (saveStatus > 0)
                {
                    return Response<NoContent>.Success(204);
                }
                return Response<NoContent>.Fail("an error occurred while adding", 500);
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        public async Task<Response<NoContent>> Update(DiscountModel model)
        {
            var status = await _dbConnection.ExecuteAsync("update discount set=@UserId,code=@Code,rate=@Rate where id=@Id", new { Id = model.Id, UserId = model.UserId, rate = model.Rate });
            if (status > 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("an error occured while updading", 404);
        }
    }
}
