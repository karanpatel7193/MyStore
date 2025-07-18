  using AdvancedADO;
using CommonLibrary;
using ECommerce.Entity.Admin.Master;
using ECommerce.Repository;
using ECommerce.Repository.Admin.Master;
using Microsoft.Extensions.Configuration;
using System.Data;
using static ECommerce.Entity.Admin.Master.CategoryEntity;

namespace ECommerce.Business.Admin.Master
{
    public class CategoryBusiness : CommonBusiness, ICategoryRepository
    {
        ISql sql;
        public CategoryBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();
        }

        public CategoryEntity MapData(IDataReader reader)
        {
            CategoryEntity categoryEntity = new CategoryEntity();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Id":
                        categoryEntity.Id = MyConvert.ToInt(reader["Id"]);
                        break;
                    case "Name":
                        categoryEntity.Name = MyConvert.ToString(reader["Name"]);
                        break;
                    case "Description":
                        categoryEntity.Description = MyConvert.ToString(reader["Description"]);
                        break;
                    case "ParentId":
                        categoryEntity.ParentId = MyConvert.ToInt(reader["ParentId"]);
                        break;
                    case "ImageUrl":
                        categoryEntity.ImageUrl = MyConvert.ToString(reader["ImageUrl"]);
                        break;
                    case "IsVisible":
                        categoryEntity.IsVisible = MyConvert.ToBoolean(reader["IsVisible"]);
                        break;
                }
            }
            return categoryEntity;
        }

        public async Task<CategoryAddEntity> SelectForAdd(CategoryParemeterEntity objParameter)
        {
            return await sql.ExecuteResultSetAsync<CategoryAddEntity>("Category_SelectForAdd", CommandType.StoredProcedure, 1, MapAddEntity);
        }

        public async Task MapAddEntity(int resultSet, CategoryAddEntity objAddEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    objAddEntity.ParentCategories.Add(await sql.MapDataAsync<CategoryMainEntity>(reader));
                    break;
            }
        }

        public async Task<CategoryEditEntity> SelectForEdit(CategoryParemeterEntity objParameter)
        {
            sql.AddParameter("Id", objParameter.Id);
            return await sql.ExecuteResultSetAsync<CategoryEditEntity>("Category_SelectForEdit", CommandType.StoredProcedure, 2, MapEditEntity);

        }

        public async Task MapEditEntity(int resultSet, CategoryEditEntity categoryEditEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    categoryEditEntity.Category = await sql.MapDataAsync<CategoryEntity>(reader);
                    break;
                case 1:
                    categoryEditEntity.ParentCategories.Add(await sql.MapDataAsync<CategoryMainEntity>(reader));
                    break;
            }
        }

        public async Task<CategoryListEntity> SelectForList(CategoryParemeterEntity categoryParemeterEntity)
        {
            CategoryGridEntity categoryGridEntity = new CategoryGridEntity();
            if (categoryParemeterEntity.Name != string.Empty)
                sql.AddParameter("Name", categoryParemeterEntity.Name);
            if (categoryParemeterEntity.ParentId != 0)
                sql.AddParameter("ParentId", categoryParemeterEntity.ParentId);
            sql.AddParameter("SortExpression", categoryParemeterEntity.SortExpression);
            sql.AddParameter("SortDirection", categoryParemeterEntity.SortDirection);
            sql.AddParameter("PageIndex", categoryParemeterEntity.PageIndex);
            sql.AddParameter("PageSize", categoryParemeterEntity.PageSize);
            return await sql.ExecuteResultSetAsync<CategoryListEntity>("Category_SelectForGrid", CommandType.StoredProcedure, 2, MapGridEntity);
        }

        public async Task MapListEntity(int resultSet, CategoryListEntity categoryListEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    categoryListEntity.Categories.Add(await sql.MapDataAsync<CategoryEntity>(reader));
                    break;
                case 1:
                    categoryListEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }

        public async Task<CategoryEntity> SelectForRecord(int Id)
        {
            sql.AddParameter("Id", Id);
            return await sql.ExecuteRecordAsync<CategoryEntity>("Category_SelectForRecord", CommandType.StoredProcedure);
        }

        public async Task<List<CategoryMainEntity>> SelectForLOV(CategoryParemeterEntity categoryParameterEntity)
        {
            return await sql.ExecuteListAsync<CategoryMainEntity>("Category_SelectForLOV", CommandType.StoredProcedure);
        }

        public async Task<CategoryGridEntity> SelectForGrid(CategoryParemeterEntity categoryParemeterEntity)
        {
            CategoryGridEntity categoryGridEntity = new CategoryGridEntity();
            if (categoryParemeterEntity.Name != string.Empty)
                sql.AddParameter("Name", categoryParemeterEntity.Name);
            if (categoryParemeterEntity.ParentId != 0)
                sql.AddParameter("ParentId", categoryParemeterEntity.ParentId);
            sql.AddParameter("SortExpression", categoryParemeterEntity.SortExpression);
            sql.AddParameter("SortDirection", categoryParemeterEntity.SortDirection);
            sql.AddParameter("PageIndex", categoryParemeterEntity.PageIndex);
            sql.AddParameter("PageSize", categoryParemeterEntity.PageSize);
            return await sql.ExecuteResultSetAsync<CategoryGridEntity>("Category_SelectForGrid", CommandType.StoredProcedure, 2, MapGridEntity);

        }
        public async Task MapGridEntity(int resultSet, CategoryGridEntity categoryGridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    categoryGridEntity.Categories.Add(await sql.MapDataAsync<CategoryEntity>(reader));
                    break;
                case 1:
                    categoryGridEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }

        public async Task<int> Insert(CategoryEntity categoryEntity)
        {
            sql.AddParameter("Name", categoryEntity.Name);
            if (categoryEntity.ParentId != 0)
                sql.AddParameter("ParentId", categoryEntity.ParentId);
            if (categoryEntity.Description != string.Empty)
                sql.AddParameter("Description", categoryEntity.Description);
            if (categoryEntity.ImageUrl != string.Empty)
                sql.AddParameter("ImageUrl", categoryEntity.ImageUrl);
            sql.AddParameter("IsVisible", categoryEntity.IsVisible);

            return MyConvert.ToInt(await sql.ExecuteScalarAsync("Category_Insert", CommandType.StoredProcedure));
        }

        public async Task<int> Update(CategoryEntity categoryEntity)
        {
            sql.AddParameter("Id", categoryEntity.Id);
            sql.AddParameter("Name", categoryEntity.Name);
            sql.AddParameter("IsVisible", categoryEntity.IsVisible);
            if (categoryEntity.ParentId != 0)
                sql.AddParameter("ParentId", categoryEntity.ParentId);
            if (categoryEntity.Description != string.Empty)
                sql.AddParameter("Description", categoryEntity.Description);
            if (categoryEntity.ImageUrl != string.Empty)
                sql.AddParameter("ImageUrl", categoryEntity.ImageUrl);

            return MyConvert.ToInt(await sql.ExecuteScalarAsync("Category_Update", CommandType.StoredProcedure));
        }

        public async Task Delete(int Id)
        {
            sql.AddParameter("Id", Id);
            await sql.ExecuteNonQueryAsync("Category_Delete", CommandType.StoredProcedure);
        }

        public async Task<List<CategoryEntity>> SelectParent()
        {
            return await sql.ExecuteListAsync<CategoryEntity>("Category_SelectParent", CommandType.StoredProcedure);
        }

        public async Task<List<CategoryEntity>> SelectChild(CategoryParemeterEntity categoryParemeterEntity)
        {
            sql.AddParameter("ParentId", categoryParemeterEntity.ParentId);
            return await sql.ExecuteListAsync<CategoryEntity>("Category_SelectChild", CommandType.StoredProcedure);
        }

        public async Task<List<CategoryPropertyValueEntity>> SelectCategoryPropertyValue()
        {

            return await sql.ExecuteListAsync<CategoryPropertyValueEntity>("CategoryPropertyValue_SelectForSyncAll", CommandType.StoredProcedure);
        }

    }
}
