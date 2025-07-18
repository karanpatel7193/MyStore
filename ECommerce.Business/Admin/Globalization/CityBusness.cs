using AdvancedADO;
using ECommerce.Entity.Admin.Globalization;
using ECommerce.Repository.Admin.Globalization;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ECommerce.Business.Admin.Globalization
{
    public class CityBusiness : CommonBusiness, ICityRepository
    {
        ISql sql;
        public CityBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }

        public async Task<List<CityMainEntity>> SelectForLOV(CityParemeterEntity cityParameterEntity)
        {
            sql.AddParameter("StateId", cityParameterEntity.StateId);
            return await sql.ExecuteListAsync<CityMainEntity>("City_SelectForLOV", CommandType.StoredProcedure);
        }


    }
}
