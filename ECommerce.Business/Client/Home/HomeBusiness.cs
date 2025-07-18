using AdvancedADO;
using CommonLibrary;
using ECommerce.Entity.Client.Home;
using ECommerce.Entity.Client.Wishlist;
using ECommerce.Repository.Client.Home;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ECommerce.Business.Client.Home
{
    public class HomeBusiness : CommonBusiness, IHomeRepository
    {
        ISql sql;

        public HomeBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }

        public async Task<List<CategoryEntity>> SelectCategoryList(bool isRedisHome)
        {

            return await sql.ExecuteListAsync<CategoryEntity>("Home_SelectForCategoryList", CommandType.StoredProcedure);

        }
        public async Task<List<BlockEntity>> SelectForBlock(bool isRedisHome)
        {

            return await sql.ExecuteListAsync<BlockEntity>("Home_selectForBlock", CommandType.StoredProcedure);
        }
        public async Task<List<BlockEntity>> SelectForSllider(bool isRedisHome)
        {

            return await sql.ExecuteListAsync<BlockEntity>("Home_selectForSlider", CommandType.StoredProcedure);
        }
        public async Task<BlockGridEntity> SelectForBlockList(bool isRedisHome)
        {
            BlockGridEntity blockGridEntity = null;
            if (isRedisHome)
            {
                try
                {
                    //TODO: Redis code pending
                }
                catch (Exception ex)
                {
                    await ex.WriteLogFileAsync();
                }
            }
            if (blockGridEntity == null)
            {
                blockGridEntity = await sql.ExecuteResultSetAsync<BlockGridEntity>("Home_SelectForBlockList", CommandType.StoredProcedure, 2, MapGridEntity);
            }
            return blockGridEntity;
        }
        public async Task MapGridEntity(int resultSet, BlockGridEntity blockGridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    blockGridEntity.Blocks.Add(await sql.MapDataAsync<BlockEntity>(reader));
                    break;
                case 1:
                    blockGridEntity.Products.Add(await sql.MapDataAsync<BlockProductEntity>(reader));
                    break;
            }
        }
       
    }
}
