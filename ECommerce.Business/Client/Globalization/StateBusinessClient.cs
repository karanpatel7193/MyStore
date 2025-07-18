using AdvancedADO;
using ECommerce.Entity.Client.Globalization;
using ECommerce.Repository.Client.Globalization;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ECommerce.Business.Client.Globalization
{
    public class StateBusinessClient : CommonBusiness, IStateRepositoryClient
    {
        ISql sql;
        public StateBusinessClient(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }

        public async Task<List<StateMainClientEnttity>> SelectForLOV(StateParemeterClientEntity stateParameterEntity)
        {
            sql.AddParameter("CountryId", stateParameterEntity.CountryId);
            return await sql.ExecuteListAsync<StateMainClientEnttity>("State_SelectForLOV", CommandType.StoredProcedure);
        }


    }
}
