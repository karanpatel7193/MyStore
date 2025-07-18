using AdvancedADO;
using CommonLibrary;
using ECommerce.Entity.Account;
using ECommerce.Entity.Client.Review;
using ECommerce.Repository.Client.Review;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ECommerce.Business.Client.Review
{
    public class ReviewSqlBusiness : CommonBusiness, IReviewRepository
    {
        ISql sql;
        public ReviewSqlBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }
        public async Task<int> Insert(ReviewEntity reviewEntity)
        {
            sql.AddParameter("UserId", reviewEntity.UserId);
            sql.AddParameter("ProductId", reviewEntity.ProductId);
            sql.AddParameter("Rating", reviewEntity.Rating);
            sql.AddParameter("Comments", reviewEntity.Comments);
            sql.AddParameter("Date", DbType.Date, ParameterDirection.Input, reviewEntity.Date);
            sql.AddParameter("MediaList", reviewEntity.MediaList.ToXML());
            return MyConvert.ToInt(await sql.ExecuteScalarAsync("Review_Insert", CommandType.StoredProcedure));
        }

        public async Task<int> Update(ReviewEntity reviewEntity)
        {
            sql.AddParameter("Id", reviewEntity.Id);
            sql.AddParameter("UserId", reviewEntity.UserId);
            sql.AddParameter("ProductId", reviewEntity.ProductId);
            sql.AddParameter("Rating", reviewEntity.Rating);
            sql.AddParameter("Date", DbType.Date, ParameterDirection.Input, reviewEntity.Date);
            sql.AddParameter("MediaList", reviewEntity.MediaList.ToXML());
            return MyConvert.ToInt(await sql.ExecuteScalarAsync("Review_Update", CommandType.StoredProcedure));
        }
        public async Task Delete(int Id)
        {
            sql.AddParameter("Id", Id);
            await sql.ExecuteNonQueryAsync("Review_Delete", CommandType.StoredProcedure);
        }

        public async Task<ReviewGridEntity> SelectForGrid(ReviewParameterEntity reviewParameterEntity)
        {
            if (reviewParameterEntity.UserId != 0)
                sql.AddParameter("UserId", reviewParameterEntity.UserId);
            if (reviewParameterEntity.ProductId != 0)
                sql.AddParameter("ProductId", reviewParameterEntity.ProductId);

            sql.AddParameter("PageIndex", reviewParameterEntity.PageIndex);
            sql.AddParameter("PageSize", reviewParameterEntity.PageSize);
            return await sql.ExecuteResultSetAsync<ReviewGridEntity>("Review_SelectForGrid", CommandType.StoredProcedure, 3, MapGridEntity);
        }

        public async Task MapGridEntity(int resultSet, ReviewGridEntity reviewGridEntity, IDataReader reader)
        {
            switch (resultSet)
            {   
                case 0:
                    reviewGridEntity.Reviews.Add(await sql.MapDataAsync<ReviewEntity>(reader));
                    break;
                case 1:
                    reviewGridEntity.MediaList.Add(await sql.MapDataAsync<ReviewMediaEntity>(reader));
                    break;
                case 2:
                    reviewGridEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;

            }
        }
    }
}
