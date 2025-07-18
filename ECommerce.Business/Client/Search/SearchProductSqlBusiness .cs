using AdvancedADO;
using CommonLibrary;
using ECommerce.Entity.Client.Search;
using ECommerce.Repository.Client.Search;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ECommerce.Business.Client.Search
{
    public class SearchProductSqlBusiness : CommonBusiness, ISearchProductRepository
    {

        ISql sql;

        public SearchProductSqlBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }

        public async Task<SearchGridEntity> SelectForCriteriea(int CategoryId)
        {
            sql.AddParameter("CategoryId", CategoryId);
            return await sql.ExecuteResultSetAsync<SearchGridEntity>("SearchProduct_SelectForCriteriea", CommandType.StoredProcedure, 2, MapForSearchCriterieaEntity);
        }

        public async Task MapForSearchCriterieaEntity(int resultSet, SearchGridEntity searchGridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    searchGridEntity.Properties.Add(await sql.MapDataAsync<PropertyMainEntity>(reader));
                    break;
                case 1:
                    searchGridEntity.Values.Add(await sql.MapDataAsync<PropertyEntity>(reader));
                    break;
            }
        }

        public Task<List<SearchPropertyEntity>> SelectForSearch(SearchProductParameterEntity searchProductParameterEntity)
        {
            throw new NotImplementedException();
        }
    }
}
