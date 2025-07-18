using CommonLibrary;
using ECommerce.Api.Common;
using ECommerce.Entity.Client.Search;
using ECommerce.Repository.Client.Search;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Client.Search
{
    [Route("client/search")]
    [ApiController]
    public class SearchProductController : ControllerBase
    {
        private readonly IConfiguration _config;

        public SearchProductController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("getForSearch", Name = "client.search.GetForSearch")]
        public async Task<Response> GetForSearch(SearchProductParameterEntity searchProductParameterEntity)
        {
            Response response;
            try
            {
                var searchProductRepository = SearchProductFactory.GetInstance(AppSettings.MongoSearch, _config);
                response = new Response(await searchProductRepository.SelectForSearch(searchProductParameterEntity));
                //var data = await searchProductRepository.SelectForSearch(searchProductParameterEntity);
                //response = new Response(data);

            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }


        [HttpPost]
        [Route("getForCriteriea", Name = "client.search.getForCriteriea")]
        public async Task<Response> GetForCriteriea(int CategoryId)
        {
            Response response;
            try
            {
                var searchProductRepository = SearchProductFactory.GetInstance(false, _config);
                response = new Response(await searchProductRepository.SelectForCriteriea(CategoryId));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
    }
}
