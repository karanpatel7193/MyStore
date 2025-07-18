using AdvancedADO;
using CommonLibrary;
using ECommerce.Entity.Admin.Homepage;
using ECommerce.Entity.Admin.Master;
using ECommerce.Repository;
using ECommerce.Repository.Admin.Homepage;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ECommerce.Business.Admin.Homepage
{
    public class BlockBusiness: CommonBusiness, IBlockRepository
    {
        ISql sql;

        public BlockBusiness(IConfiguration config) : base(config)
        {
            sql = CreateSqlInstance();

        }

        #region Public Methods

        public BlockEntity MapData(IDataReader reader)
        {
            BlockEntity blockEntity = new BlockEntity();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Id":
                        blockEntity.Id = MyConvert.ToInt(reader["Id"]);
                        break;
                    case "Name":
                        blockEntity.Name = MyConvert.ToString(reader["Name"]);
                        break;
                    case "Description":
                        blockEntity.Description = MyConvert.ToString(reader["Description"]);
                        break;
                    case "Content":
                        blockEntity.Content = MyConvert.ToString(reader["Content"]);
                        break;
                    case "IsActive":
                        blockEntity.IsActive = MyConvert.ToBoolean(reader["IsActive"]);
                        break;
                }
            }
            return blockEntity;
        }

        public async Task<BlockEntity> SelectForRecord(int Id)
        {
            sql.AddParameter("Id", Id);
            return await sql.ExecuteRecordAsync<BlockEntity>("Block_SelectForRecord", CommandType.StoredProcedure);
        }

        public async Task<List<BlockMainEntity>> SelectForLOV(BlockParameterEntity blockParameterEntity)
        {
            return await sql.ExecuteListAsync<BlockMainEntity>("Block_SelectForLOV", CommandType.StoredProcedure);
        }

        public async Task<BlockGridEntity> SelectForGrid(BlockParameterEntity blockParameterEntity)
        {
            BlockGridEntity blockGridEntity = new BlockGridEntity();
            if (blockParameterEntity.Name != string.Empty)
                sql.AddParameter("Name", blockParameterEntity.Name);
            if (blockParameterEntity.Description != string.Empty)
                sql.AddParameter("Description", blockParameterEntity.Description);
            if (blockParameterEntity.IsActive != false)
                sql.AddParameter("IsActive", blockParameterEntity.IsActive);
            sql.AddParameter("SortExpression", blockParameterEntity.SortExpression);
            sql.AddParameter("SortDirection", blockParameterEntity.SortDirection);
            sql.AddParameter("PageIndex", blockParameterEntity.PageIndex);
            sql.AddParameter("PageSize", blockParameterEntity.PageSize);

            return await sql.ExecuteResultSetAsync<BlockGridEntity>("Block_SelectForGrid", CommandType.StoredProcedure, 2, MapGridEntity);
        }

        public async Task MapGridEntity(int resultSet, BlockGridEntity blockGridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    blockGridEntity.Blocks.Add(await sql.MapDataAsync<BlockEntity>(reader));
                    break;
                case 1:
                    blockGridEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }

        public async Task<int> Insert(BlockEntity blockEntity)
        {
            sql.AddParameter("Name", blockEntity.Name);
            sql.AddParameter("IsActive", blockEntity.IsActive);
            if (blockEntity.Description != string.Empty)
                sql.AddParameter("Description", blockEntity.Description);
            if (blockEntity.Content != string.Empty)
                sql.AddParameter("Content", blockEntity.Content);
            if (blockEntity.BlockProducts != null && blockEntity.BlockProducts.Count > 0)
                sql.AddParameter("BlockProductsXML", blockEntity.BlockProducts.ToXML());

            return MyConvert.ToInt(await sql.ExecuteScalarAsync("Block_Insert", CommandType.StoredProcedure));
        }

        public async Task<int> Update(BlockEntity blockEntity)
        {
            sql.AddParameter("Id", blockEntity.Id);
            sql.AddParameter("Name", blockEntity.Name);
            sql.AddParameter("IsActive", blockEntity.IsActive);
            if (blockEntity.Description != string.Empty)
                sql.AddParameter("Description", blockEntity.Description);
            if (blockEntity.Content != string.Empty)
                sql.AddParameter("Content", blockEntity.Content);
            if (blockEntity.BlockProducts != null && blockEntity.BlockProducts.Count > 0)
                sql.AddParameter("BlockProductsXML", blockEntity.BlockProducts.ToXML());

            return MyConvert.ToInt(await sql.ExecuteScalarAsync("Block_Update", CommandType.StoredProcedure));
        }

        public async Task Delete(int Id)
        {
            sql.AddParameter("Id", Id);
            await sql.ExecuteNonQueryAsync("Block_Delete", CommandType.StoredProcedure);
        }

        public async Task<BlockAddEntity> SelectForAdd(BlockParameterEntity objParameter)
        {
            return await sql.ExecuteResultSetAsync<BlockAddEntity>("Block_SelectForAdd", CommandType.StoredProcedure, 1, MapAddEntity);

        }

        public async Task MapAddEntity(int resultSet, BlockAddEntity blockAddEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    blockAddEntity.Products.Add(await sql.MapDataAsync<ProductMainEntity>(reader));
                    break;
            }
        }

        public async Task<BlockEditEntity> SelectForEdit(BlockParameterEntity blockParameterEntity)
        {
            sql.AddParameter("Id", blockParameterEntity.Id);

            return await sql.ExecuteResultSetAsync<BlockEditEntity>("Block_SelectForEdit", CommandType.StoredProcedure, 3, MapEditEntity);
        }

        public async Task MapEditEntity(int resultSet, BlockEditEntity blockEditEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    blockEditEntity.Block = await sql.MapDataAsync<BlockEntity>(reader);
                    break;
                case 1:
                    blockEditEntity.Block.BlockProducts.Add(await sql.MapDataAsync<BlockProductEntity>(reader));
                    break;
                case 2:
                    blockEditEntity.Products.Add(await sql.MapDataAsync<ProductMainEntity>(reader));
                    break;
            }
        }

        public async Task<BlockListEntity> SelectForList(BlockParameterEntity blockParameterEntity)
        {
            BlockListEntity blockListEntity = new BlockListEntity();
            if (blockParameterEntity.Name != string.Empty)
                sql.AddParameter("Name", blockParameterEntity.Name);
            if (blockParameterEntity.Description != string.Empty)
                sql.AddParameter("Description", blockParameterEntity.Description);
            if (blockParameterEntity.IsActive != false)
                sql.AddParameter("IsActive", blockParameterEntity.IsActive);
            sql.AddParameter("SortExpression", blockParameterEntity.SortExpression);
            sql.AddParameter("SortDirection", blockParameterEntity.SortDirection);
            sql.AddParameter("PageIndex", blockParameterEntity.PageIndex);
            sql.AddParameter("PageSize", blockParameterEntity.PageSize);
            return await sql.ExecuteResultSetAsync<BlockListEntity>("Block_SelectForList", CommandType.StoredProcedure, 2, MapListEntity);
        }

        public async Task MapListEntity(int resultSet, BlockListEntity blockListEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    blockListEntity.Blocks.Add(await sql.MapDataAsync<BlockEntity>(reader));
                    break;
                case 1:
                    blockListEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }
        #endregion
    }
}
