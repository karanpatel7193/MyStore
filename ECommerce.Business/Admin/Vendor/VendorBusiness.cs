using AdvancedADO;
using CommonLibrary;
using ECommerce.Entity.Admin.Globalization;
using ECommerce.Entity.Admin.Vendor;
using ECommerce.Repository.Admin.Vendor;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ECommerce.Business.Admin.Vendor
{
    public class VendorBusiness : CommonBusiness, IVendorRepository
    {
        ISql sql;
        public VendorBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }

        public VendorEntity MapData(IDataReader reader)
        {
            VendorEntity vendorEntity = new VendorEntity();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Id":
                        vendorEntity.Id = MyConvert.ToInt(reader["Id"]);
                        break;
                    case "Name":
                        vendorEntity.Name = MyConvert.ToString(reader["Name"]);
                        break;
                    case "Email":
                        vendorEntity.Email = MyConvert.ToString(reader["Email"]);
                        break;
                    case "Phone":
                        vendorEntity.Phone = MyConvert.ToLong(reader["Phone"]);
                        break;
                    case "Address":
                        vendorEntity.Address = MyConvert.ToString(reader["Address"]);
                        break;
                    case "StateId":
                        vendorEntity.StateId = MyConvert.ToInt(reader["StateId"]);
                        break;
                    case "CountryId":
                        vendorEntity.CountryId = MyConvert.ToInt(reader["CountryId"]);
                        break;
                    case "PostalCode":
                        vendorEntity.PostalCode = MyConvert.ToString(reader["PostalCode"]);
                        break;
                    case "Status":
                        vendorEntity.Status = MyConvert.ToBoolean(reader["Status"]);
                        break;
                    case "TaxNumber":
                        vendorEntity.TaxNumber = MyConvert.ToString(reader["TaxNumber"]);
                        break;
                    case "BankAccountNumber":
                        vendorEntity.BankAccountNumber = MyConvert.ToString(reader["BankAccountNumber"]);
                        break;
                    case "BankName":
                        vendorEntity.BankName = MyConvert.ToString(reader["BankName"]);
                        break;
                    case "IFSCCode":
                        vendorEntity.IFSCCode = MyConvert.ToString(reader["IFSCCode"]);
                        break;
                    case "ContactPersonName":
                        vendorEntity.ContactPersonName = MyConvert.ToString(reader["ContactPersonName"]);
                        break;
                    case "ContactPersonPhone":
                        vendorEntity.ContactPersonPhone = MyConvert.ToString(reader["ContactPersonPhone"]);
                        break;
                    case "CreatedOn":
                        vendorEntity.CreatedOn = MyConvert.ToDateTime(reader["CreatedOn"]);
                        break;
                    case "TotalOutstanding":
                        vendorEntity.TotalOutstanding = MyConvert.ToDecimal(reader["TotalOutstanding"]);
                        break;
                    case "TotalPaid":
                        vendorEntity.TotalPaid = MyConvert.ToDecimal(reader["TotalPaid"]);
                        break;
                    case "TotalInvoices":
                        vendorEntity.TotalInvoices = MyConvert.ToDecimal(reader["TotalInvoices"]);
                        break;
                }
            }
            return vendorEntity;
        }

        public async Task<VendorAddEntity> SelectForAdd(VendorParameterEntity vendorParameterEntity)
        {
            return await sql.ExecuteResultSetAsync<VendorAddEntity>("Vendor_SelectForAdd", CommandType.StoredProcedure, 1, MapAddEntity);
        }

        public async Task MapAddEntity(int resultSet, VendorAddEntity vendorAddEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    vendorAddEntity.Countries.Add(await sql.MapDataAsync<CountryMainEntity>(reader));
                    break;
            }
        }

        public async Task<VendorEditEntity> SelectForEdit(VendorParameterEntity vendorParameterEntity)
        {
            sql.AddParameter("Id", vendorParameterEntity.Id);
            return await sql.ExecuteResultSetAsync<VendorEditEntity>("Vendor_SelectForEdit", CommandType.StoredProcedure, 3, MapEditEntity);

        }

        public async Task MapEditEntity(int resultSet, VendorEditEntity vendorEditEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    vendorEditEntity.Vendor = await sql.MapDataAsync<VendorEntity>(reader);
                    break;
                case 1:
                    vendorEditEntity.Countries.Add(await sql.MapDataAsync<CountryMainEntity>(reader));
                    break;
                case 2:
                    vendorEditEntity.States.Add(await sql.MapDataAsync<StateMainEntity>(reader));
                    break;
            }
        }

        public async Task<VendorListEntity> SelectForList(VendorParameterEntity vendorParemeterEntity)
        {
            VendorGridEntity vendorGridEntity = new VendorGridEntity();

            if (vendorParemeterEntity.Name != string.Empty)
                sql.AddParameter("Name", vendorParemeterEntity.Name);
            if (vendorParemeterEntity.Email != string.Empty)
                sql.AddParameter("Email", vendorParemeterEntity.Email);
            if (vendorParemeterEntity.Phone != 0)
                sql.AddParameter("Phone", vendorParemeterEntity.Phone);
            if (vendorParemeterEntity.StateId != 0)
                sql.AddParameter("StateId", vendorParemeterEntity.StateId);
            if (vendorParemeterEntity.CountryId != 0)
                sql.AddParameter("CountryId", vendorParemeterEntity.CountryId);
            if (vendorParemeterEntity.PostalCode != string.Empty)
                sql.AddParameter("PostalCode", vendorParemeterEntity.PostalCode);

            sql.AddParameter("SortExpression", vendorParemeterEntity.SortExpression);
            sql.AddParameter("SortDirection", vendorParemeterEntity.SortDirection);
            sql.AddParameter("PageIndex", vendorParemeterEntity.PageIndex);
            sql.AddParameter("PageSize", vendorParemeterEntity.PageSize);
            return await sql.ExecuteResultSetAsync<VendorListEntity>("Vendor_SelectForList", CommandType.StoredProcedure, 4, MapGridEntity);
        }

        public async Task MapListEntity(int resultSet, VendorListEntity vendorListEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    vendorListEntity.Vendors.Add(await sql.MapDataAsync<VendorEntity>(reader));
                    break;
                case 1:
                    vendorListEntity.Countries.Add(await sql.MapDataAsync<CountryMainEntity>(reader));
                    break;
                case 2:
                    vendorListEntity.States.Add(await sql.MapDataAsync<StateMainEntity>(reader));
                    break;
                case 3:
                    vendorListEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }

        public async Task<VendorEntity> SelectForRecord(int Id)
        {
            sql.AddParameter("Id", Id);
            return await sql.ExecuteRecordAsync<VendorEntity>("Vendor_SelectForRecord", CommandType.StoredProcedure);
        }

        public async Task<List<VendorMainEntity>> SelectForLOV(VendorParameterEntity vendorParameterEntity)
        {
            return await sql.ExecuteListAsync<VendorMainEntity>("Vendor_SelectForLOV", CommandType.StoredProcedure);
        }

        public async Task<VendorGridEntity> SelectForGrid(VendorParameterEntity vendorParemeterEntity)
        {
            VendorGridEntity vendorGridEntity = new VendorGridEntity();
            if (vendorParemeterEntity.Name != string.Empty)
                sql.AddParameter("Name", vendorParemeterEntity.Name);
            if (vendorParemeterEntity.Email != string.Empty)
                sql.AddParameter("Email", vendorParemeterEntity.Email);
            if (vendorParemeterEntity.Phone != 0)
                sql.AddParameter("Phone", vendorParemeterEntity.Phone);
            if (vendorParemeterEntity.StateId != 0)
                sql.AddParameter("StateId", vendorParemeterEntity.StateId);
            if (vendorParemeterEntity.CountryId != 0)
                sql.AddParameter("CountryId", vendorParemeterEntity.CountryId);
            if (vendorParemeterEntity.PostalCode != string.Empty)
                sql.AddParameter("PostalCode", vendorParemeterEntity.PostalCode);
            sql.AddParameter("SortExpression", vendorParemeterEntity.SortExpression);
            sql.AddParameter("SortDirection", vendorParemeterEntity.SortDirection);
            sql.AddParameter("PageIndex", vendorParemeterEntity.PageIndex);
            sql.AddParameter("PageSize", vendorParemeterEntity.PageSize);
            return await sql.ExecuteResultSetAsync<VendorGridEntity>("Vendor_SelectForGrid", CommandType.StoredProcedure, 2, MapGridEntity);

        }
        public async Task MapGridEntity(int resultSet, VendorGridEntity vendorGridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    vendorGridEntity.Vendors.Add(await sql.MapDataAsync<VendorEntity>(reader));
                    break;
                case 1:
                    vendorGridEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }

        public async Task<int> Insert(VendorEntity vendorEntity)
        {
            sql.AddParameter("Name", vendorEntity.Name);

            // Handling nullable fields and adding them to the parameters
            if (vendorEntity.Email != string.Empty)
                sql.AddParameter("Email", vendorEntity.Email);
            if (vendorEntity.Phone != 0)
                sql.AddParameter("Phone", vendorEntity.Phone);
            if (vendorEntity.Address != string.Empty)
                sql.AddParameter("Address", vendorEntity.Address);
            if (vendorEntity.StateId != 0)
                sql.AddParameter("StateId", vendorEntity.StateId);
            if (vendorEntity.CountryId != 0)
                sql.AddParameter("CountryId", vendorEntity.CountryId);
            if (vendorEntity.PostalCode != string.Empty)
                sql.AddParameter("PostalCode", vendorEntity.PostalCode);
            if (vendorEntity.TaxNumber != string.Empty)
                sql.AddParameter("TaxNumber", vendorEntity.TaxNumber);
            if (vendorEntity.BankAccountNumber != string.Empty)
                sql.AddParameter("BankAccountNumber", vendorEntity.BankAccountNumber);
            if (vendorEntity.BankName != string.Empty)
                sql.AddParameter("BankName", vendorEntity.BankName);
            if (vendorEntity.IFSCCode != string.Empty)
                sql.AddParameter("IFSCCode", vendorEntity.IFSCCode);
            if (vendorEntity.ContactPersonName != string.Empty)
                sql.AddParameter("ContactPersonName", vendorEntity.ContactPersonName);
            if (vendorEntity.ContactPersonPhone != string.Empty)
                sql.AddParameter("ContactPersonPhone", vendorEntity.ContactPersonPhone);

            sql.AddParameter("CreatedOn", DbType.DateTime, ParameterDirection.Input, vendorEntity.CreatedOn);
            sql.AddParameter("TotalOutstanding", vendorEntity.TotalOutstanding);
            sql.AddParameter("TotalPaid", vendorEntity.TotalPaid);
            sql.AddParameter("TotalInvoices", vendorEntity.TotalInvoices);
            sql.AddParameter("Status", vendorEntity.Status);

            return MyConvert.ToInt(await sql.ExecuteScalarAsync("Vendor_Insert", CommandType.StoredProcedure));
        }
        public async Task<int> Update(VendorEntity vendorEntity)
        {
            sql.AddParameter("Id", vendorEntity.Id);
            sql.AddParameter("Name", vendorEntity.Name);

            // Handling nullable fields and adding them to the parameters
            if (vendorEntity.Email != string.Empty)
                sql.AddParameter("Email", vendorEntity.Email);

            if (vendorEntity.Phone != 0)
                sql.AddParameter("Phone", vendorEntity.Phone);

            if (vendorEntity.Address != string.Empty)
                sql.AddParameter("Address", vendorEntity.Address);

            if (vendorEntity.StateId != 0)
                sql.AddParameter("StateId", vendorEntity.StateId);

            if (vendorEntity.CountryId != 0)
                sql.AddParameter("CountryId", vendorEntity.CountryId);

            if (vendorEntity.PostalCode != string.Empty)
                sql.AddParameter("PostalCode", vendorEntity.PostalCode);

            if (vendorEntity.TaxNumber != string.Empty)
                sql.AddParameter("TaxNumber", vendorEntity.TaxNumber);

            if (vendorEntity.BankAccountNumber != string.Empty)
                sql.AddParameter("BankAccountNumber", vendorEntity.BankAccountNumber);

            if (vendorEntity.BankName != string.Empty)
                sql.AddParameter("BankName", vendorEntity.BankName);

            if (vendorEntity.IFSCCode != string.Empty)
                sql.AddParameter("IFSCCode", vendorEntity.IFSCCode);

            if (vendorEntity.ContactPersonName != string.Empty)
                sql.AddParameter("ContactPersonName", vendorEntity.ContactPersonName);

            if (vendorEntity.ContactPersonPhone != string.Empty)
                sql.AddParameter("ContactPersonPhone", vendorEntity.ContactPersonPhone);

            sql.AddParameter("CreatedOn", DbType.DateTime, ParameterDirection.Input, vendorEntity.CreatedOn);
            sql.AddParameter("TotalOutstanding", vendorEntity.TotalOutstanding);
            sql.AddParameter("TotalPaid", vendorEntity.TotalPaid);
            sql.AddParameter("TotalInvoices", vendorEntity.TotalInvoices);
            sql.AddParameter("Status", vendorEntity.Status);

            return MyConvert.ToInt(await sql.ExecuteScalarAsync("Vendor_Update", CommandType.StoredProcedure));
        }
        public async Task Delete(int Id)
        {
            sql.AddParameter("Id", Id);
            await sql.ExecuteNonQueryAsync("Vendor_Delete", CommandType.StoredProcedure);
        }

        //other method

        public async Task<List<StateMainEntity>> SelectForStateLOV(VendorParameterEntity vendorParameterEntity)
        {
             sql.AddParameter("CountryId", vendorParameterEntity.CountryId);
            return await sql.ExecuteListAsync<StateMainEntity>("State_SelectForLOV", CommandType.StoredProcedure);
        }
    }
}
