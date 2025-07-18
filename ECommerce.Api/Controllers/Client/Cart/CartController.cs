using CommonLibrary;
using DocumentFormat.OpenXml.Office2010.Excel;
using ECommerce.Api.Common;
using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Client.Cart;
using ECommerce.Repository.Admin.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Client.Cart
{
    [Route("client/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        ICartRepository cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }
        [HttpPost]
        [Route("insert", Name = "client.cart.insert")]
        //[AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.Insert)]
        public async Task<Response> Insert(CartEntity cartEntity)
        {
            Response response;
            try
            {
                response = new Response( await cartRepository.Insert(cartEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
        [HttpPost]
        [Route("insertBulk", Name = "client.cart.insertBulk")]
        //[AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.Insert)]
        public async Task<Response> InsertBulk(CartMainEntity cartMainEntity)
        {
            Response response;
            try
            {
                response = new Response(await cartRepository.InsertBulk(cartMainEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
        [HttpPost]
        [Route("update", Name = "client.cart.update")]
        //[AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.Insert)]
        public async Task<Response> Update(CartEntity cartEntity)
        {
            Response response;
            try
            {
                response = new Response(await cartRepository.Update(cartEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
        [HttpPost]
        [Route("delete", Name = "client.cart.delete")]
        //[AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.Insert)]
        public async Task<Response> Delete(CartParameterEntity cartParameterEntity)
        {
            Response response;
            try
            {
                await cartRepository.Delete(cartParameterEntity);
                response = new Response();
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
        [HttpPost]
        [Route("getForGrid", Name = "client.cart.getForGrid")]
        //[AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.Insert)]
        public async Task<Response> GetForGrid(CartParameterEntity cartParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await cartRepository.SelectForGrid(cartParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
    }
}
