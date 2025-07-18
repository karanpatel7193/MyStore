using ECommerce.Repository.Client.Product;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Business.Client.Product
{
    public class ProductDetailsFactory 
    {
        public static IProductDetailsRepository GetInstance(bool isMongoUsed,bool isRedis, IConfiguration config)
        {
            if (isMongoUsed)
                return new ProductDetailsMongoBusiness(config);
            else if (isRedis)
                return new ProductDetailsRedisBusiness(config);
            else
                return new ProductDetailsSqlBusiness(config);
        }

    }
}
