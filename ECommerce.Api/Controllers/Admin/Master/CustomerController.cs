using CommonLibrary;
using ECommerce.Entity.Admin.Master;
using ECommerce.Repository;
using ECommerce.Repository.Admin.Master;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Admin.Master
{
    [Route("admin/customer")]
    [ApiController]
    public class CustomerController : ControllerBase, IPageControllerPartial<CustomerEntity, CustomerParameterEntity, int>
    {
        public readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpPost]
        [Route("getForGrid", Name = "admin.customer.getForGrid")]
        public async Task<Response> GetForGrid(CustomerParameterEntity parameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await customerRepository.SelectForGrid(parameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpGet]
        [Route("getRecord/{id:int}", Name = "admin.customer.getRecord")]
        public async Task<Response> GetForRecord(int id)
        {
            Response response;
            try
            {
                response = new Response(await customerRepository.SelectForRecord(id));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("insert", Name = "admin.customer.insert")]
        public async Task<Response> Insert(CustomerEntity customerEntity)
        {
            Response response;
            try
            {
                response = new Response(await customerRepository.Insert(customerEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("update", Name = "admin.customer.update")]
        public async Task<Response> Update(CustomerEntity customerEntity)
        {
            Response response;
            try
            {
                response = new Response(await customerRepository.Update(customerEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("delete/{id:int}", Name = "admin.customer.delete")]
        public async Task<Response> Delete(int id)
        {
            Response response;
            try
            {
                await customerRepository.Delete(id);
                response = new Response();
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getForLOV", Name = "admin.customer.getForLOV")]
        public async Task<Response> GetForLOV(CustomerParameterEntity customerParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await customerRepository.SelectForLOV(customerParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
    }
}
