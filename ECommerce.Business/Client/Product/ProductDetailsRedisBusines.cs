using ECommerce.Entity.Client.Product;
using ECommerce.Repository.Client.Product;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Business.Client.Product
{
    public class ProductDetailsRedisBusiness : CommonBusiness, IProductDetailsRepository
    {
        public ProductDetailsRedisBusiness(IConfiguration config) : base(config)
        {
        }


        public Task<ProductDetailsGridEntity> SelectForProductDetails(ProductDetailsPatameterEntity productDetailsPatameterEntitySS)
        {
            throw new NotImplementedException();
        }
    }
}
