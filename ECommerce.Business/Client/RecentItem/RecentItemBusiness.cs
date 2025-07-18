using AdvancedADO;
using CommonLibrary;
using ECommerce.Entity.Client.RecentItem;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ECommerce.Business.Client.RecentItem
{
    public class RecentItemBusiness : CommonBusiness
    {
        ISql sql;
        public RecentItemBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }

        #region Public Methods

        public RecentItemEntity MapData(IDataReader reader)
        {
            RecentItemEntity recentItemEntity = new RecentItemEntity();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Id":
                        recentItemEntity.Id = MyConvert.ToInt(reader["Id"]);
                        break;
                    case "ProductId":
                        recentItemEntity.ProductId = MyConvert.ToInt(reader["ProductId"]);
                        break;
                    case "UserId":
                        recentItemEntity.UserId = MyConvert.ToLong(reader["UserId"]);
                        break;
                }
            }
            return recentItemEntity;
        }

        public async Task<RecentItemGridEntity> SelectForGrid(RecentItemParameterEntity recentItemParamterEntity)
        {
            RecentItemGridEntity recentItemGridEntity = new RecentItemGridEntity();
            if (recentItemParamterEntity.ProductId != 0)
                sql.AddParameter("ProductId", recentItemParamterEntity.ProductId); 
            if (recentItemParamterEntity.UserId != 0)
                sql.AddParameter("UserId", recentItemParamterEntity.UserId);

            sql.AddParameter("SortExpression", recentItemParamterEntity.SortExpression);
            sql.AddParameter("SortDirection", recentItemParamterEntity.SortDirection);
            sql.AddParameter("PageIndex", recentItemParamterEntity.PageIndex);
            sql.AddParameter("PageSize", recentItemParamterEntity.PageSize);

            return await sql.ExecuteResultSetAsync<RecentItemGridEntity>("RecentItem_SelectForGrid", CommandType.StoredProcedure, 2, MapGridEntity);

        }

        public async Task MapGridEntity(int resultSet, RecentItemGridEntity recentItemGridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    recentItemGridEntity.RecentItems.Add(await sql.MapDataAsync<RecentItemEntity>(reader));
                    break;
                case 1:
                    recentItemGridEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }

        public async Task<int> Insert(RecentItemEntity recentItemEntity)
        {
            sql.AddParameter("ProductId", recentItemEntity.ProductId);
            sql.AddParameter("UserId", recentItemEntity.UserId);
            return MyConvert.ToInt(await sql.ExecuteScalarAsync("RecentItem_Insert", CommandType.StoredProcedure));
        }

        public async Task<int> Update(RecentItemEntity recentItemEntity)
        {
            sql.AddParameter("Id", recentItemEntity.Id);
            sql.AddParameter("ProductId", recentItemEntity.ProductId);
            sql.AddParameter("UserId", recentItemEntity.UserId);
            return MyConvert.ToInt(await sql.ExecuteScalarAsync("RecentItem_Update", CommandType.StoredProcedure));
        }

        public async Task Delete(int Id)
        {
            sql.AddParameter("Id", Id);
            await sql.ExecuteNonQueryAsync("RecentItem_Delete", CommandType.StoredProcedure);
        }

        #endregion
    }
}
