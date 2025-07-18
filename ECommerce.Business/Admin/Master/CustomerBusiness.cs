using AdvancedADO;
using CommonLibrary;
using ECommerce.Entity.Admin.Master;
using ECommerce.Repository;
using ECommerce.Repository.Admin.Master;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Admin.Master
{
    public class CustomerBusiness : CommonBusiness, ICustomerRepository
    {
        ISql sql;

        public CustomerBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }

        public CustomerEntity MapData(IDataReader reader)
        {
            CustomerEntity customerEntity = new CustomerEntity();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Id":
                        customerEntity.Id = MyConvert.ToInt(reader["Id"]);
                        break;
                    case "Name":
                        customerEntity.Name = MyConvert.ToString(reader["Name"]);
                        break;
                    case "TotalBuy":
                        customerEntity.TotalBuy = MyConvert.ToInt(reader["Id"]);
                        break;
                    case "TotalInvoices":
                        customerEntity.TotalInvoices = MyConvert.ToString(reader["Id"]);
                        break;
                    case "Status":
                        customerEntity.Status = MyConvert.ToInt(reader["Id"]);
                        break;
                    case "UserId":
                        customerEntity.UserId = MyConvert.ToInt(reader["Id"]);
                        break;
                    case "UserName":
                        customerEntity.UserName = MyConvert.ToString(reader["Id"]);
                        break;
                }
            }
            return customerEntity;
        }


        public async Task<CustomerGridEntity> SelectForGrid(CustomerParameterEntity objParameter)
        {
            if (!string.IsNullOrEmpty(objParameter.Name))
                sql.AddParameter("Name", objParameter.Name);

            if (objParameter.UserId != 0)
                sql.AddParameter("UserId", objParameter.UserId);

            sql.AddParameter("SortExpression", objParameter.SortExpression);
            sql.AddParameter("SortDirection", objParameter.SortDirection);
            sql.AddParameter("PageIndex", objParameter.PageIndex);
            sql.AddParameter("PageSize", objParameter.PageSize);

            return await sql.ExecuteResultSetAsync<CustomerGridEntity>("Customer_SelectForGrid", CommandType.StoredProcedure, 2, MapGridEntity);
        }

        public async Task MapGridEntity(int resultSet, CustomerGridEntity customerGridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    customerGridEntity.Customers.Add(await sql.MapDataAsync<CustomerEntity>(reader));
                    break;
                case 1:
                    customerGridEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }

        public Task<CustomerEntity> SelectForRecord(int objPK)
        {
            throw new NotImplementedException();
        }

        public Task<List<CustomerMainEntity>> SelectForLOV(CustomerParameterEntity objParameter)
        {
            throw new NotImplementedException();
        }

        public Task<int> Insert(CustomerEntity objEAL)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(CustomerEntity objEAL)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int objPK)
        {
            throw new NotImplementedException();
        }
    }
}
