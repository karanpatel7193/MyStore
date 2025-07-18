using CommonLibrary;
using ECommerce.Entity.Admin.Order.OrderInvoice;
using ECommerce.Repository;
using ECommerce.Repository.Admin.Invoice.OrderInvoice.Invoice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Admin.Invoice.OrderInvoice
{
    [Route("invoice/purchaseInvoice")]
    [ApiController]
    public class PurchaseInvoiceController : ControllerBase, IPageController<PurchaseInvoiceEntity, PurchaseInvoiceParameterEntity, int>
    {
        IPurchaseInvoiceRepository purchaseInvoiceRepository;

        public PurchaseInvoiceController(IPurchaseInvoiceRepository purchaseInvoiceRepository)
        {
            this.purchaseInvoiceRepository = purchaseInvoiceRepository;
        }

        [HttpPost]
        [Route("delete/{Id:int}", Name = "invoice.purchaseInvoice.delete")]
        public async Task<Response> Delete(int Id)
        {
            Response response;
            try
            {
                await purchaseInvoiceRepository.Delete(Id);
                response = new Response();
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getAddMode", Name = "invoice.purchaseInvoice.getAddMode")]
        public async Task<Response> GetForAdd(PurchaseInvoiceParameterEntity purchaseInvoiceParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await purchaseInvoiceRepository.SelectForAdd(purchaseInvoiceParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
        [HttpPost]
        [Route("getEditMode", Name = "invoice.purchaseInvoice.getEditMode")]
        public async Task<Response> GetForEdit(PurchaseInvoiceParameterEntity purchaseInvoiceParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await purchaseInvoiceRepository.SelectForEdit(purchaseInvoiceParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getForGrid", Name = "invoice.purchaseInvoice.getForGrid")]
        public async Task<Response> GetForGrid(PurchaseInvoiceParameterEntity purchaseInvoiceParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await purchaseInvoiceRepository.SelectForGrid(purchaseInvoiceParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getListValue", Name = "invoice.purchaseInvoice.getListValue")]
        public async Task<Response> GetForList(PurchaseInvoiceParameterEntity purchaseInvoiceParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await purchaseInvoiceRepository.SelectForList(purchaseInvoiceParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getLovValue", Name = "invoice.purchaseInvoice.getLovValue")]
        public async Task<Response> GetForLOV(PurchaseInvoiceParameterEntity purchaseInvoiceParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await purchaseInvoiceRepository.SelectForLOV(purchaseInvoiceParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpGet]
        [Route("getRecord/{Id:int}", Name = "invoice.purchaseInvoice.getRecord")]
        public async Task<Response> GetForRecord(int Id)
        {
            Response response;
            try
            {
                response = new Response(await purchaseInvoiceRepository.SelectForRecord(Id));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("insert", Name = "invoice.purchaseInvoice.insert")]
        public async Task<Response> Insert(PurchaseInvoiceEntity purchaseInvoiceEntity)
        {
            Response response;
            try
            {
                int result = await purchaseInvoiceRepository.Insert(purchaseInvoiceEntity);
                response = new Response(result);
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("update", Name = "admin.purchaseInvoice.update")]
        public async Task<Response> Update(PurchaseInvoiceEntity purchaseInvoiceEntity)
        {
            Response response;
            try
            {
                int result = await purchaseInvoiceRepository.Update(purchaseInvoiceEntity);
                response = new Response(result);
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
    }
}
