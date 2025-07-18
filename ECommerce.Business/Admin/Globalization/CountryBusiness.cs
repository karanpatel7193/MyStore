using AdvancedADO;
using ECommerce.Business;
using ECommerce.Repository.Admin.Globalization;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ECommerce.Entity.Admin.Globalization
{
    public class CountryBusiness : CommonBusiness, ICountryRepository
    {
        ISql sql;
        public CountryBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }
   
        public async Task<List<CountryMainEntity>> SelectForLOV(CountryParemeterEntity countryParameterEntity)
        {
            return await sql.ExecuteListAsync<CountryMainEntity>("Country_SelectForLOV", CommandType.StoredProcedure);
        }

    }
}
