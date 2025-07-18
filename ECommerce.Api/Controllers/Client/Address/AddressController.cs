using CommonLibrary;
using DocumentFormat.OpenXml.Office2010.Excel;
using ECommerce.Api.Common;
using ECommerce.Entity.Client.Address;
using ECommerce.Entity.Client.Cart;
using ECommerce.Repository.Account;
using ECommerce.Repository.Client.Address;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Client.Address
{
    [Route("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        IAddressRepository addressRepository ;

        public AddressController(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }
        [HttpPost]
        [Route("insert", Name = "client.address.insert")]
        //[AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.Insert)]
        public async Task<Response> Insert(AddressEntity addressEntity)
        {
            Response response;
            try
            {
                response = new Response(await addressRepository.Insert(addressEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("update", Name = "client.address.update")]
        //[AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.Insert)]
        public async Task<Response> Update(AddressEntity addressEntity)
        {
            Response response;
            try
            {
                response = new Response(await addressRepository.Update(addressEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
        [HttpGet]
        [Route("getRecord/{id:int}", Name = "client.address.record")]
        //[AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.View)]

        public async Task<Response> GetForRecord(int id)
        {
            Response response;
            try
            {

                response = new Response(await addressRepository.SelectForRecord(id));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
        [HttpPost]
        [Route("delete", Name = "client.address.delete")]
        //[AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.View)]

        public async Task<Response> Delete(AddressParameterEntity addressParameterEntity)
        {
            Response response;
            try
            {
                await addressRepository.Delete(addressParameterEntity);
                response = new Response();
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
        [HttpPost]
        [Route("getForGrid", Name = "client.address.getForGrid")]
        //[AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.View)]

        public async Task<Response> GetForGrid(AddressParameterEntity addressParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await addressRepository.SelectForGrid(addressParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
    }
}
