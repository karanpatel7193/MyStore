using AdvancedADO;
using CommonLibrary;
using ECommerce.Business;
using ECommerce.Repository.Client.Address;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace ECommerce.Entity.Client.Address
{
    public class AddressBusiness : CommonBusiness, IAddressRepository
    {
        ISql sql;

        public AddressBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }
        public async Task<AddressEntity> SelectForRecord(int Id)
        {
            sql.AddParameter("Id", Id);
            return await sql.ExecuteRecordAsync<AddressEntity>("Address_SelectForRecord", CommandType.StoredProcedure);
        }
        public async Task<int> Insert(AddressEntity addressEntity)
        {
            sql.AddParameter("UserId", addressEntity.UserId);
            sql.AddParameter("FullName", addressEntity.FullName);
            sql.AddParameter("MobileNumber", addressEntity.MobileNumber);
            sql.AddParameter("AlternateNumber", addressEntity.AlternateNumber);
            sql.AddParameter("AddressLine", addressEntity.AddressLine);
            sql.AddParameter("Landmark", addressEntity.Landmark);
            sql.AddParameter("CityId", addressEntity.CityId);
            sql.AddParameter("StateId", addressEntity.StateId);
            sql.AddParameter("PinCode", addressEntity.PinCode);
            sql.AddParameter("AddressType", addressEntity.AddressType);
            sql.AddParameter("IsDefault", addressEntity.IsDefault);

            return MyConvert.ToInt(await sql.ExecuteScalarAsync("Address_Insert", CommandType.StoredProcedure));

        }

        public async Task<int> Update(AddressEntity addressEntity)
        {
            sql.AddParameter("Id", addressEntity.Id);
            sql.AddParameter("UserId", addressEntity.UserId);
            sql.AddParameter("FullName", addressEntity.FullName);
            sql.AddParameter("MobileNumber", addressEntity.MobileNumber);
            sql.AddParameter("AlternateNumber", addressEntity.AlternateNumber);
            sql.AddParameter("AddressLine", addressEntity.AddressLine);
            sql.AddParameter("Landmark", addressEntity.Landmark);
            sql.AddParameter("CityId", addressEntity.CityId);
            sql.AddParameter("StateId", addressEntity.StateId);
            sql.AddParameter("PinCode", addressEntity.PinCode);
            sql.AddParameter("AddressType", addressEntity.AddressType);
            sql.AddParameter("IsDefault", addressEntity.IsDefault);

            return MyConvert.ToInt(await sql.ExecuteScalarAsync("Address_Update", CommandType.StoredProcedure));
        }

        public async Task Delete(AddressParameterEntity addressParameterEntity)
        {
            sql.AddParameter("Id", addressParameterEntity.Id);
            sql.AddParameter("UserId", addressParameterEntity.UserId);
            await sql.ExecuteNonQueryAsync("Address_Delete", CommandType.StoredProcedure);
        }

        public async Task<AddressGridEntity> SelectForGrid(AddressParameterEntity addressParameterEntity)
        {
            AddressGridEntity addressGridEntity = new AddressGridEntity();
            sql.AddParameter("UserId", addressParameterEntity.UserId);

            return await sql.ExecuteResultSetAsync<AddressGridEntity>("Address_SelectForUser", CommandType.StoredProcedure, 2, MapGridEntity);
        }

        public async Task MapGridEntity(int resultSet, AddressGridEntity addressGridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    addressGridEntity.Addresses.Add(await sql.MapDataAsync<AddressEntity>(reader));
                    break;
                case 1:
                    addressGridEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }
    }
}