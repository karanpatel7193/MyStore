using AdvancedADO;
using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Client.Product;
using ECommerce.Repository.Client.Product;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ECommerce.Business.Client.Product
{
    public class ProductDetailsSqlBusiness : CommonBusiness, IProductDetailsRepository
    {
        ISql sql;

        public ProductDetailsSqlBusiness(IConfiguration config): base (config) { 
            sql = CreateSqlInstance();
        }

        public async Task<ProductDetailsGridEntity> SelectForProductDetails(ProductDetailsPatameterEntity productDetailsPatameterEntity)
        {
            sql.AddParameter("Id", productDetailsPatameterEntity.Id);
            return await sql.ExecuteResultSetAsync<ProductDetailsGridEntity>("Product_SelectForDetails", CommandType.StoredProcedure, 4, MapGridEntity);
        }


        public async Task MapGridEntity(int resultSet, ProductDetailsGridEntity productDetailsGridEntity, IDataReader reader)
        {
            switch (resultSet)
            {
                case 0:
                    productDetailsGridEntity.ProductDetails.Add(await sql.MapDataAsync<ProductDetailsEntity>(reader));
                    break;
            }
            switch (resultSet)
            {
                case 1:
                    productDetailsGridEntity.ProductDetailsSpecification.Add(await sql.MapDataAsync<ProductDetailsSpecificationEntity>(reader)); 
                    break;
            }
            switch (resultSet)
            {
                case 2:
                    productDetailsGridEntity.ProductVarient.Add(await sql.MapDataAsync<ProductVarientEntity>(reader));
                    break;
            }
            switch (resultSet)
            {
                case 3:
                    productDetailsGridEntity.ProductAllVarient.Add(await sql.MapDataAsync<ProductAllVarientEntity>(reader));
                    break;
            }

        }

    }
}
