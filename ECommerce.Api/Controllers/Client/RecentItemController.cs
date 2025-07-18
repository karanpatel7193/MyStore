using CommonLibrary;
using ECommerce.Business.Client.RecentItem;
using ECommerce.Entity.Client.RecentItem;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Client
{
    [Route("client/recentItem")]
    [ApiController]
    public class RecentItemController : ControllerBase
    {
        public RecentItemController()
        {
        }

        [HttpPost]
        [Route("getForGrid", Name = "client.recentItem.getForGrid")]
        public async Task<Response> GetForGrid(RecentItemParameterEntity recentItemParameterEntity)
        {
            Response response;
            try
            {
                RecentItemBusiness recentItemBusiness = new RecentItemBusiness(Startup.Configuration);
                response = new Response(await recentItemBusiness.SelectForGrid(recentItemParameterEntity));

            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("insert", Name = "client.recentItem.insert")]
        public async Task<Response> Insert(RecentItemEntity recentItemEntity)
        {
            Response response;
            try
            {
                RecentItemBusiness recentItemBusiness = new RecentItemBusiness(Startup.Configuration);
                response = new Response(await recentItemBusiness.Insert(recentItemEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }


        [HttpPost]
        [Route("update", Name = "client.recentItem.update")]
        public async Task<Response> Update(RecentItemEntity recentItemEntity)
        {
            Response response;
            try
            {
                RecentItemBusiness recentItemBusiness = new RecentItemBusiness(Startup.Configuration);
                int id = await recentItemBusiness.Update(recentItemEntity);
                response = new Response(id);
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("delete/{Id:int}", Name = "client.recentItem.delete")]
        public async Task<Response> Delete(int Id)
        {
            Response response;
            try
            {

                RecentItemBusiness recentItemBusiness = new RecentItemBusiness(Startup.Configuration);
                await recentItemBusiness.Delete(Id);
                response = new Response();
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }



    }
}
