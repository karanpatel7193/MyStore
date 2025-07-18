using AdvancedADO;
using CommonLibrary;
using ECommerce.Entity.Account;
using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Client.Cart;
using ECommerce.Entity.Client.Wishlist;
using ECommerce.Repository.Account;
using ECommerce.Repository.Client.Wishlist;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ECommerce.Business.Client.Wishlist
{
    public class WishlistBusiness : CommonBusiness, IWishlistRepository
    {
        ISql sql;
        public WishlistBusiness(IConfiguration configuration) : base(configuration)
        {
            sql = CreateSqlInstance();
        }

        public WishlistEntity MapData(IDataReader reader)
        {
            WishlistEntity wishlistEntity = new WishlistEntity();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                switch (reader.GetName(i))
                {
                    case "Id":
                        wishlistEntity.Id = MyConvert.ToInt(reader["Id"]);
                        break;
                    case "UserId":
                        wishlistEntity.UserId = MyConvert.ToInt(reader["UserId"]);
                        break;
                    case "ProductId":
                        wishlistEntity.ProductId = MyConvert.ToInt(reader["ProductId"]);
                        break;
                    //case "CreatedTime":
                    //    wishlistEntity.CreatedTime = MyConvert.ToDateTime(reader["CreatedTime"]);
                    //    break;
                }
            }
            return wishlistEntity;
        }

        public async Task<WishlistEntity> SelectForRecord(int Id)
        {
            sql.AddParameter("Id", Id);
            return await sql.ExecuteRecordAsync<WishlistEntity>("Wishlist_SelectForRecord", CommandType.StoredProcedure);
        }
        public async Task<WishlistGridEntity> SelectForGrid(WishlistParameterEntity wishlistParameterEntity)
        {
            if (wishlistParameterEntity.UserId != 0)
                sql.AddParameter("UserId", wishlistParameterEntity.UserId);

            sql.AddParameter("SortExpression", wishlistParameterEntity.SortExpression);
            sql.AddParameter("SortDirection", wishlistParameterEntity.SortDirection);
            sql.AddParameter("PageIndex", wishlistParameterEntity.PageIndex);
            sql.AddParameter("PageSize", wishlistParameterEntity.PageSize);
            return await sql.ExecuteResultSetAsync<WishlistGridEntity>("Wishlist_SelectForGrid", CommandType.StoredProcedure, 2, MapGridEntity);
        }

        public async Task MapGridEntity(int resultSet, WishlistGridEntity wishlistGridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                //case 0:
                //    wishlistGridEntity.Wishlists.Add(await sql.MapDataAsync<WishlistEntity>(reader));
                //    break;
                case 0:
                    wishlistGridEntity.Wishlists.Add(await sql.MapDataAsync<WishlistProductEntity>(reader));
                    break;
                case 1:
                    wishlistGridEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }

        public async Task<int> Insert(WishlistEntity wishlistEntity)
        {
            sql.AddParameter("UserId", wishlistEntity.UserId);
            sql.AddParameter("ProductId", wishlistEntity.ProductId);
            //sql.AddParameter("CreatedTime", DbType.DateTime,ParameterDirection.Input ,wishlistEntity.CreatedTime);
            
            return MyConvert.ToInt(await sql.ExecuteScalarAsync("Wishlist_Insert", CommandType.StoredProcedure));
        }

        public async Task<int> InsertBulk(WishlistMainEntity wishlistMainEntity)
        {
            sql.AddParameter("UserId", wishlistMainEntity.UserId);
            sql.AddParameter("Wishlist", wishlistMainEntity.Wishlist.ToXML());
            return MyConvert.ToInt(await sql.ExecuteScalarAsync("Wishlist_InsertBulk", CommandType.StoredProcedure));
        }

        public async Task<int> Update(WishlistEntity wishlistEntity)
        {
            sql.AddParameter("UserId", wishlistEntity.UserId);
            sql.AddParameter("ProductId", wishlistEntity.ProductId);
            //sql.AddParameter("CreatedTime", DbType.DateTime, ParameterDirection.Input, wishlistEntity.CreatedTime);

            return MyConvert.ToInt(await sql.ExecuteScalarAsync("Wishlist_Update", CommandType.StoredProcedure));
        }

        public async Task DeleteWishlist(WishlistParameterEntity wishlistParameterEntity)
        {
            sql.AddParameter("UserId", wishlistParameterEntity.UserId);
            sql.AddParameter("ProductId", wishlistParameterEntity.ProductId);
            await sql.ExecuteNonQueryAsync("Wishlist_Delete", CommandType.StoredProcedure);
        }
        public async Task CheckIfProductInWishlist(WishlistParameterEntity wishlistParameterEntity)
        {
            sql.AddParameter("UserId", wishlistParameterEntity.UserId);
            await sql.ExecuteNonQueryAsync("Wishlist_CheckIfProductInWishlist", CommandType.StoredProcedure);
        }
        public Task<WishlistAddEntity> SelectForAdd(WishlistParameterEntity objParameter)
        {
            throw new NotImplementedException();
        }

        public Task MapAddEntity(int resultSet, WishlistAddEntity objAddEntity, IDataReader reader)
        {
            throw new NotImplementedException();
        }

        public Task<WishlistEditEntity> SelectForEdit(WishlistParameterEntity objParameter)
        {
            throw new NotImplementedException();
        }

        public Task MapEditEntity(int resultSet, WishlistEditEntity objEditEntity, IDataReader reader)
        {
            throw new NotImplementedException();
        }

        public Task<WishlistListEntity> SelectForList(WishlistParameterEntity objParameter)
        {
            throw new NotImplementedException();
        }

        public Task MapListEntity(int resultSet, WishlistListEntity objListEntity, IDataReader reader)
        {
            throw new NotImplementedException();
        }

        public Task<List<WishlistMainEntity>> SelectForLOV(WishlistParameterEntity objParameter)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int objPK)
        {
            throw new NotImplementedException();
        }
    }
}
