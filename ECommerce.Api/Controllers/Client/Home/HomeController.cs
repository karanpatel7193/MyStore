using CommonLibrary;
using ECommerce.Api.Common;
using ECommerce.Business.Client.Home;
using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Client.Home;
using ECommerce.Entity.Client.Wishlist;
using ECommerce.Entity.Common;
using ECommerce.Repository.Client.Home;
using ECommerce.Repository.Client.Wishlist;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Client.Home
{
    [Route("client/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public readonly IHomeRepository homeRepository;

        public HomeController(IHomeRepository homeRepository)
        {
            this.homeRepository = homeRepository;
        }

        [HttpPost]
        [Route("getBlockList", Name = "client.home.getBlockList")]
        public async Task<Response> GetForBlockList()
        {
            Response response;
            try
            {
                response = new Response(await homeRepository.SelectForBlockList(Common.AppSettings.RedisHome));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getBlock", Name = "client.home.getBlock")]
        public async Task<Response> GetForBlock()
        {
            Response response;
            try
            {
                response = new Response(await homeRepository.SelectForBlock(Common.AppSettings.RedisHome));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getCategoryList", Name = "client.home.getCategoryList")]
        public async Task<Response> GetForCategoryList()
        {
            Response response;
            try
            {
                response = new Response(await homeRepository.SelectCategoryList(Common.AppSettings.RedisHome));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        //slider 

        [HttpPost]
        [Route("getSlider", Name = "client.home.getSlider")]
        public async Task<Response> GetForSlider()
        {
            Response response;
            try
            {
                response = new Response(await homeRepository.SelectForSllider(Common.AppSettings.RedisHome));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
    }
}

