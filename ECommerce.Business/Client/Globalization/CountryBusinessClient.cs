using AdvancedADO;
using ECommerce.Business;
using ECommerce.Repository.Client.Globalization;
using ECommerce.Entity.Client.Globalization;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ECommerce.Entity.Client.Globalization
{
    public class CountryBusinessClient : CommonBusiness, ICountryRepositoryClient
    {
        ISql sql;
        public CountryBusinessClient(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }
   
        public async Task<List<CountryMainClientEnttity>> SelectForLOV(CountryParemeterClientEntity countryParameterEntity)
        {
            return await sql.ExecuteListAsync<CountryMainClientEnttity>("Country_SelectForLOV", CommandType.StoredProcedure);
        }

    }
}
