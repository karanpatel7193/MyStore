using CommonLibrary;
using ECommerce.Api.Common;
using ECommerce.Business.Client.Product;
using ECommerce.Entity.Client.Product;
using ECommerce.Entity.Common;
using Microsoft.AspNetCore.Mvc;


namespace ECommerce.Api.Controllers.Client.Product
{
    [Route("client/product")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ProductDetailsController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost]
        [Route("getDetails", Name = "client.product.getDetails")]
        public async Task<Response> GetProductDetails(ProductDetailsPatameterEntity productDetailsPatameterEntity)
        {
            Response response;
            try
            {

                var productDetailsRepository = ProductDetailsFactory.GetInstance(Common.AppSettings.MongoProduct,Common.AppSettings.RedisProduct, _config);
                response = new Response(await productDetailsRepository.SelectForProductDetails(productDetailsPatameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
    }
}
