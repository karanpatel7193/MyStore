using CommonLibrary;
using ECommerce.Api.Common;
using ECommerce.Entity.Admin.Vendor;
using ECommerce.Repository;
using ECommerce.Repository.Admin.Vendor;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Admin.Vendor
{
    [Route("admin/vendor")]
    [ApiController]
    public class VendorController : ControllerBase, IPageController<VendorEntity, VendorParameterEntity, int>
    {
        IVendorRepository vendorRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public VendorController(IWebHostEnvironment hostingEnvironment, IVendorRepository vendorRepository)
        {
            this.vendorRepository = vendorRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [Route("getForGrid", Name = "admin.vendor.getForGrid")]
        [AuthorizeAPI(pageName: "VendorList", pageAccess: PageAccessValues.View)]

        public async Task<Response> GetForGrid(VendorParameterEntity vendorParemeterEntity)
        {
            Response response;
            try
            {
                response = new Response(await vendorRepository.SelectForGrid(vendorParemeterEntity));

            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpGet]
        [Route("getRecord/{Id:int}", Name = "admin.vendor.getRecord")]
        [AuthorizeAPI(pageName: "VendorList", pageAccess: PageAccessValues.View)]

        public async Task<Response> GetForRecord(int Id)
        {
            Response response;
            try
            {
                response = new Response(await vendorRepository.SelectForRecord(Id));

            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("insert", Name = "admin.vendor.insert")]
        [AuthorizeAPI(pageName: "VendorList", pageAccess: PageAccessValues.Insert)]

        public async Task<Response> Insert(VendorEntity vendorEntity)
        {
            Response response;
            try
            {
                response = new Response(await vendorRepository.Insert(vendorEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }


        [HttpPost]
        [Route("update", Name = "admin.vendor.update")]
        [AuthorizeAPI(pageName: "VendorList", pageAccess: PageAccessValues.Update)]

        public async Task<Response> Update(VendorEntity vendorEntity)
        {
            Response response;
            try
            {
               response = new Response(await vendorRepository.Update(vendorEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("delete/{Id:int}", Name = "admin.vendor.delete")]
        [AuthorizeAPI(pageName: "VendorList", pageAccess: PageAccessValues.Delete)]

        public async Task<Response> Delete(int Id)
        {
            Response response;
            try
            {
                await vendorRepository.Delete(Id);
                response = new Response();
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getAddMode", Name = "admin.vendor.getAddMode")]
        [AuthorizeAPI(pageName: "VendorList", pageAccess: PageAccessValues.Insert)]

        public async Task<Response> GetForAdd(VendorParameterEntity vendorParemeterEntity)
        {
            Response response;
            try
            {
                response = new Response(await vendorRepository.SelectForAdd(vendorParemeterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getEditMode", Name = "admin.vendor.getEditMode")]
        [AuthorizeAPI(pageName: "VendorList", pageAccess: PageAccessValues.Update)]

        public async Task<Response> GetForEdit(VendorParameterEntity vendorParemeterEntity)
        {
            Response response;
            try
            {
                response = new Response(await vendorRepository.SelectForEdit(vendorParemeterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getLovValue", Name = "admin.vendor.getLovValue")]
        [AuthorizeAPI(pageName: "VendorList", pageAccess: PageAccessValues.IgnoreAuthorization)]

        public async Task<Response> GetForLOV(VendorParameterEntity vendorParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await vendorRepository.SelectForLOV(vendorParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getListValue", Name = "admin.vendor.getListValue")]
        [AuthorizeAPI(pageName: "VendorList", pageAccess: PageAccessValues.View)]

        public async Task<Response> GetForList(VendorParameterEntity vendorParemeterEntity)
        {
            Response response;
            try
            {
                response = new Response(await vendorRepository.SelectForList(vendorParemeterEntity));

            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        //other method

        
    }
}
