using AdvancedADO;
using ECommerce.Entity.Admin.Globalization;
using ECommerce.Repository.Admin.Globalization;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ECommerce.Business.Admin.Globalization
{
    public class StateBusiness : CommonBusiness, IStateRepository
    {
        ISql sql;
        public StateBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }

        public async Task<List<StateMainEntity>> SelectForLOV(StateParemeterEntity stateParameterEntity)
        {
            sql.AddParameter("CountryId", stateParameterEntity.CountryId);
            return await sql.ExecuteListAsync<StateMainEntity>("State_SelectForLOV", CommandType.StoredProcedure);
        }


    }
}
