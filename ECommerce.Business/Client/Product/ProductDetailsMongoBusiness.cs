using DocumentDBClient;
using ECommerce.Entity.Client.Product;
using ECommerce.Entity.Client.Search;
using ECommerce.Repository.Client.Product;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Business.Client.Product
{
    public class ProductDetailsMongoBusiness : CommonBusiness, IProductDetailsRepository
    {
        private readonly IDocument<ProductMongoEntity> document;

        public ProductDetailsMongoBusiness(IConfiguration config) : base(config)
        {
            document = CreateDocumentInstance<ProductMongoEntity>("DefaultMongoDB", "product");
        }

        public async Task<ProductDetailsGridEntity> SelectForProductDetails(ProductDetailsPatameterEntity parameter)
        {
            var result = await document.GetByIdAsync(parameter.Id.ToString());

            if (result == null)
                return null;

            // Map basic product details
            var productDetails = new ProductDetailsEntity
            {
                Id = int.TryParse(result.Id, out var parsedId) ? parsedId : 0,
                Name = result.Name,
                Description = result.Description,
                SKU = result.SKU,
                SellPrice = result.SellPrice,
                DiscountPercentage = result.DiscountPercentage,
                FinalSellPrice = result.FinalSellPrice,
                CategoryName = result.CategoryName,
                Url = result.Url,
                ThumbUrl = result.ThumbUrl
            };

            // Map product specifications
            var specificationList = new List<ProductDetailsSpecificationEntity>();

            if (result.Specifications != null)
            {
                foreach (var spec in result.Specifications)
                {
                    specificationList.Add(new ProductDetailsSpecificationEntity
                    {
                        PropertyName = spec.PropertyName,
                        Unit = spec.Unit,
                        Value = spec.Value
                    });
                }
            }

            // Return final grid entity
            return new ProductDetailsGridEntity
            {
                ProductDetails = new List<ProductDetailsEntity> { productDetails },
                ProductDetailsSpecification = specificationList
            };
        }
    }
}
