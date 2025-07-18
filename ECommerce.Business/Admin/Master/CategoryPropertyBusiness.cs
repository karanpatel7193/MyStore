using AdvancedADO;
using CommonLibrary;
using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Admin.Master.CategoryProperty;
using ECommerce.Repository;
using ECommerce.Repository.Admin.Master.CategoryProperty;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading.Tasks;
using static ECommerce.Entity.Admin.Master.CategoryEntity;

namespace ECommerce.Business.Admin.Master
{
    public class CategoryPropertyBusiness : CommonBusiness, ICategoryPropertyRepository, IBusiness<CategoryPropertyEntity, CategoryPropertyMainEntity, CategoryPropertyAddEntity, CategoryPropertyEditEntity, CategoryPropertyListEntity, CategoryPropertyGridEntity, CategoryPropertyParameterEntity, int>
    {
        public readonly ISql sql;

        public CategoryPropertyBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }

        public CategoryPropertyEntity MapData(IDataReader reader)
        {
            var categoryPropertyEntity = new CategoryPropertyEntity();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Id":
                        categoryPropertyEntity.Id = MyConvert.ToInt(reader["Id"]);
                        break;
                    case "CategoryId":
                        categoryPropertyEntity.CategoryId = MyConvert.ToInt(reader["CategoryId"]);
                        break;
                    case "PropertyId":
                        categoryPropertyEntity.PropertyId = MyConvert.ToInt(reader["PropertyId"]);
                        break;
                    case "Unit":
                        categoryPropertyEntity.Unit = MyConvert.ToString(reader["Unit"]);
                        break;
                    case "CategoryName":
                        categoryPropertyEntity.CategoryName = MyConvert.ToString(reader["Unit"]);
                        break;
                }
            }
            return categoryPropertyEntity;
        }

        public async Task<CategoryPropertyEntity> SelectForRecord(int id)
        {
            sql.AddParameter("Id", id);
            return await sql.ExecuteRecordAsync<CategoryPropertyEntity>("CategoryProperty_SelectForRecord", CommandType.StoredProcedure);
        }
        public async Task<List<CategoryPropertyEntity>> SelectForCategoryProperty(CategoryPropertyParameterEntity categoryPropertyParameterEntity)
        {
            sql.AddParameter("CategoryId", categoryPropertyParameterEntity.CategoryId);
            return await sql.ExecuteListAsync<CategoryPropertyEntity>("CategoryProperty_SelectByCategory", CommandType.StoredProcedure);
        }
        public async Task<CategoryPropertyGridEntity> SelectForGrid(CategoryPropertyParameterEntity categoryPropertyParameterEntity)
        {
            if (categoryPropertyParameterEntity.CategoryId != 0)
                sql.AddParameter("CategoryId", categoryPropertyParameterEntity.CategoryId);
            if (categoryPropertyParameterEntity.PropertyId != 0)
                sql.AddParameter("PropertyId", categoryPropertyParameterEntity.PropertyId);
            if (categoryPropertyParameterEntity.Unit != string.Empty)
                sql.AddParameter("Unit", categoryPropertyParameterEntity.Unit);
            sql.AddParameter("SortExpression", categoryPropertyParameterEntity.SortExpression);
            sql.AddParameter("SortDirection", categoryPropertyParameterEntity.SortDirection);
            sql.AddParameter("PageIndex", categoryPropertyParameterEntity.PageIndex);
            sql.AddParameter("PageSize", categoryPropertyParameterEntity.PageSize);

            return await sql.ExecuteResultSetAsync<CategoryPropertyGridEntity>("CategoryProperty_SelectForGrid", CommandType.StoredProcedure, 2, MapGridEntity);
        }

        public async Task MapGridEntity(int resultSet, CategoryPropertyGridEntity categoryPropertyGridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    categoryPropertyGridEntity.CategoryPropertys.Add(await sql.MapDataAsync<CategoryPropertyEntity>(reader));
                    break;
                case 1:
                    categoryPropertyGridEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }

        public async Task<int> Insert(CategoryPropertyEntity categoryPropertyEntity)
        {
            sql.AddParameter("CategoryId", categoryPropertyEntity.CategoryId);
            sql.AddParameter("PropertyId", categoryPropertyEntity.PropertyId);
            sql.AddParameter("Unit", categoryPropertyEntity.Unit);
            return MyConvert.ToInt(await sql.ExecuteScalarAsync("CategoryProperty_Insert", CommandType.StoredProcedure));
        }

        public async Task<int> Update(CategoryPropertyEntity categoryPropertyEntity)
        {
            sql.AddParameter("Id", categoryPropertyEntity.Id);
            sql.AddParameter("CategoryId", categoryPropertyEntity.CategoryId);
            sql.AddParameter("PropertyId", categoryPropertyEntity.PropertyId);
            sql.AddParameter("Unit", categoryPropertyEntity.Unit);
            return MyConvert.ToInt(await sql.ExecuteScalarAsync("CategoryProperty_Update", CommandType.StoredProcedure));
        }

        public async Task Delete(int id)
        {
            sql.AddParameter("Id", id);
            await sql.ExecuteNonQueryAsync("CategoryProperty_Delete", CommandType.StoredProcedure);
        }

        public async Task<List<CategoryPropertyMainEntity>> SelectForLOV(CategoryPropertyParameterEntity categoryPropertyParameterEntity)
        {
            return await sql.ExecuteListAsync<CategoryPropertyMainEntity>("CategoryProperty_SelectForLov", CommandType.StoredProcedure);

        }
 
        public async Task<CategoryPropertyAddEntity> SelectForAdd(CategoryPropertyParameterEntity categoryPropertyParameterEntity)
        {
            return await sql.ExecuteResultSetAsync<CategoryPropertyAddEntity>("CategoryProperty_SelectForAdd", CommandType.StoredProcedure, 1, MapAddEntity);

        }
        public async Task MapAddEntity(int resultSet, CategoryPropertyAddEntity objAddEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    objAddEntity.Properties.Add(await sql.MapDataAsync<PropertyMainEntity>(reader));
                    break;
            }
        }

        public async Task<CategoryPropertyEditEntity> SelectForEdit(CategoryPropertyParameterEntity objParameter)
        {
            sql.AddParameter("Id", objParameter.CategoryId);
            return await sql.ExecuteResultSetAsync<CategoryPropertyEditEntity>("CategoryProperty_SelectForEdit", CommandType.StoredProcedure, 2, MapEditEntity);
        }

        public async Task MapEditEntity(int resultSet, CategoryPropertyEditEntity categoryPropertyEditEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    categoryPropertyEditEntity.CategoryProperty = await sql.MapDataAsync<CategoryPropertyEntity>(reader);
                    break;
                case 1:
                    categoryPropertyEditEntity.Properties.Add(await sql.MapDataAsync<PropertyMainEntity>(reader));
                    break;
            }
        }


        public async Task<CategoryPropertyListEntity> SelectForList(CategoryPropertyParameterEntity categoryPropertyParameterEntity)
        {
          

            if (categoryPropertyParameterEntity.CategoryId != 0)
                sql.AddParameter("CategoryId", categoryPropertyParameterEntity.CategoryId);

            if (categoryPropertyParameterEntity.PropertyId != 0)
                sql.AddParameter("PropertyId", categoryPropertyParameterEntity.PropertyId);

            if (categoryPropertyParameterEntity.Unit != string.Empty)
                sql.AddParameter("Unit", categoryPropertyParameterEntity.Unit);

            sql.AddParameter("SortExpression", categoryPropertyParameterEntity.SortExpression);
            sql.AddParameter("SortDirection", categoryPropertyParameterEntity.SortDirection);
            sql.AddParameter("PageIndex", categoryPropertyParameterEntity.PageIndex);
            sql.AddParameter("PageSize", categoryPropertyParameterEntity.PageSize);
            return await sql.ExecuteResultSetAsync<CategoryPropertyListEntity>("CategoryProperty_SelectForList", CommandType.StoredProcedure, 2, MapGridEntity);
        }

        public async Task MapListEntity(int resultSet, CategoryPropertyListEntity CategoryPropertyListEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    CategoryPropertyListEntity.CategoryPropertys.Add(await sql.MapDataAsync<CategoryPropertyEntity>(reader));
                    break;
                case 1:
                    CategoryPropertyListEntity.CategoryPropertys.Add(await sql.MapDataAsync<CategoryPropertyEntity>(reader));
                    break;
                case 3:
                    CategoryPropertyListEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }
        public Task MapGridEntity(int resultSet, CategoryPropertyListEntity objGridEntity, IDataReader reader)
        {
            throw new NotImplementedException();
        }

        async Task<object> ICategoryPropertyRepository.SelectForGrid(CategoryPropertyParameterEntity categoryPropertyParameterEntity)
        {
            if (categoryPropertyParameterEntity.CategoryId != 0)
                sql.AddParameter("CategoryId", categoryPropertyParameterEntity.CategoryId);
            if (categoryPropertyParameterEntity.PropertyId != 0)
                sql.AddParameter("PropertyId", categoryPropertyParameterEntity.PropertyId);
            if (categoryPropertyParameterEntity.Unit != string.Empty)
                sql.AddParameter("Unit", categoryPropertyParameterEntity.Unit);

            sql.AddParameter("SortExpression", categoryPropertyParameterEntity.SortExpression);
            sql.AddParameter("SortDirection", categoryPropertyParameterEntity.SortDirection);
            sql.AddParameter("PageIndex", categoryPropertyParameterEntity.PageIndex);
            sql.AddParameter("PageSize", categoryPropertyParameterEntity.PageSize);

            return await sql.ExecuteResultSetAsync<CategoryPropertyGridEntity>("CategoryProperty_SelectForGrid", CommandType.StoredProcedure, 2, MapGridEntity);
        }
       
        Task<CategoryPropertyListEntity> IBusinessPartial<CategoryPropertyEntity, CategoryPropertyMainEntity, CategoryPropertyListEntity, CategoryPropertyParameterEntity, int>.SelectForGrid(CategoryPropertyParameterEntity objParameter)
        {
            throw new NotImplementedException();
        }

        Task<CategoryPropertyGridEntity> IBusiness<CategoryPropertyEntity, CategoryPropertyMainEntity, CategoryPropertyAddEntity, CategoryPropertyEditEntity, CategoryPropertyListEntity, CategoryPropertyGridEntity, CategoryPropertyParameterEntity, int>.SelectForList(CategoryPropertyParameterEntity objParameter)
        {
            throw new NotImplementedException();
        }

        public Task MapListEntity(int resultSet, CategoryPropertyGridEntity objListEntity, IDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}