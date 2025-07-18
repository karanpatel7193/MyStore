using AdvancedADO;
using Microsoft.Extensions.Configuration;
using System.Data;
using ECommerce.Repository.Client.Globalization;
using ECommerce.Entity.Client.Globalization;

namespace ECommerce.Business.Client.Globalization
{
    public class CityBusinessClient : CommonBusiness, ICityRepositoryClient
    {
        ISql sql;
        public CityBusinessClient(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }

        public async Task<List<CityMainClientEnttity>> SelectForLOV(CityParemeterClientEntity cityParameterEntity)
        {
            sql.AddParameter("StateId", cityParameterEntity.StateId);
            return await sql.ExecuteListAsync<CityMainClientEnttity>("City_SelectForLOV", CommandType.StoredProcedure);
        }


    }
}
