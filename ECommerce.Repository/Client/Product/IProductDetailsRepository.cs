using ECommerce.Entity.Client.Product;

namespace ECommerce.Repository.Client.Product
{
    public interface IProductDetailsRepository
    {
        public Task<ProductDetailsGridEntity> SelectForProductDetails(ProductDetailsPatameterEntity productDetailsPatameterEntitySS);

    }
}
