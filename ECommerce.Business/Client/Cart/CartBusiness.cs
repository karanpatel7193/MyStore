using AdvancedADO;
using CommonLibrary;
using DocumentDBClient;
using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Client.Cart;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ECommerce.Business.Client.Cart
{
    public class CartBusiness:CommonBusiness, ICartRepository
    {
        ISql sql;

        public CartBusiness(IConfiguration confif):base(confif)
        {
            sql = CreateSqlInstance();
        }
        public async Task<int> Insert(CartEntity cartEntity)
        {
            sql.AddParameter("UserId", cartEntity.UserId);
            sql.AddParameter("ProductId", cartEntity.ProductId);
            sql.AddParameter("Quantity", cartEntity.Quantity);
            sql.AddParameter("AddedDate", DbType.DateTime, ParameterDirection.Input, cartEntity.AddedDate);
            cartEntity.Id = MyConvert.ToInt(await sql.ExecuteScalarAsync("Cart_Insert", CommandType.StoredProcedure));
            return cartEntity.Id;
        }
        public async Task<int> InsertBulk(CartMainEntity cartMainEntity)
        {
            sql.AddParameter("UserId", cartMainEntity.UserId);
            sql.AddParameter("CartXML", cartMainEntity.CartItems.ToXML());
            return MyConvert.ToInt(await sql.ExecuteScalarAsync("Cart_InsertBulk", CommandType.StoredProcedure));
        }

        public async Task<int> Update(CartEntity cartEntity)
        {
            sql.AddParameter("UserId", cartEntity.UserId);
            sql.AddParameter("ProductId", cartEntity.ProductId);
            sql.AddParameter("Quantity", cartEntity.Quantity);

            return MyConvert.ToInt(await sql.ExecuteScalarAsync("Cart_Update", CommandType.StoredProcedure));
        }
        public async Task Delete(CartParameterEntity cartParameterEntity)
        {
            sql.AddParameter("UserId", cartParameterEntity.UserId);
            sql.AddParameter("ProductId", cartParameterEntity.ProductId);
            await sql.ExecuteNonQueryAsync("Cart_Delete", CommandType.StoredProcedure);
        }
        public async Task<CartGridEntity> SelectForGrid(CartParameterEntity cartParameterEntity)
        {
            CartGridEntity CartGridEntity = new CartGridEntity();
            if (cartParameterEntity.ProductName != string.Empty)    
                sql.AddParameter("ProductName", cartParameterEntity.ProductName);
            sql.AddParameter("UserId", cartParameterEntity.UserId);
            sql.AddParameter("SortExpression", cartParameterEntity.SortExpression);
            sql.AddParameter("SortDirection", cartParameterEntity.SortDirection);
            sql.AddParameter("PageIndex", cartParameterEntity.PageIndex);
            sql.AddParameter("PageSize", cartParameterEntity.PageSize);
            return await sql.ExecuteResultSetAsync<CartGridEntity>("Cart_SelectForGrid", CommandType.StoredProcedure, 2, MapGridEntity);
        }
        
        public async Task MapGridEntity(int resultSet, CartGridEntity CartGridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    CartGridEntity.Products.Add(await sql.MapDataAsync<CartEntity>(reader));
                    break;
                case 1:
                    CartGridEntity.TotalRecords = MyConvert.ToInt(reader["TotalRecords"]);
                    break;
            }
        }
    }
}
