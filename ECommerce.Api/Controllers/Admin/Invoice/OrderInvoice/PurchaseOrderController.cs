using CommonLibrary;
using ECommerce.Api.Common;
using ECommerce.Entity.Admin.Master.CategoryProperty;
using ECommerce.Entity.Admin.Order.OrderInvoice;
using ECommerce.Repository;
using ECommerce.Repository.Admin.Invoice.OrderInvoice;
using ECommerce.Repository.Admin.Master.CategoryProperty;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Admin.Invoice.OrderInvoice
{
    [Route("invoice/purchaseOrder")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase, IPageController<PurchaseOrderEntity, PurchaseOrderParameterEntity, int>
    {
        IPurchaseOrderRepository purchaseOrderRepository;

        public PurchaseOrderController(IPurchaseOrderRepository purchaseOrderRepository)
        {
            this.purchaseOrderRepository = purchaseOrderRepository;
        }

        [HttpPost]
        [Route("delete/{Id:int}", Name = "invoice.purchaseOrder.delete")]
        public async Task<Response> Delete(int Id)
        {
            Response response;
            try
            {
                await purchaseOrderRepository.Delete(Id);
                response = new Response();
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getAddMode", Name = "invoice.purchaseOrder.getAddMode")]
        public async Task<Response> GetForAdd(PurchaseOrderParameterEntity purchaseOrderParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await purchaseOrderRepository.SelectForAdd(purchaseOrderParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
        [HttpPost]
        [Route("getEditMode", Name = "invoice.purchaseOrder.getEditMode")]
        public async Task<Response> GetForEdit(PurchaseOrderParameterEntity purchaseOrderParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await purchaseOrderRepository.SelectForEdit(purchaseOrderParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getForGrid", Name = "invoice.purchaseOrder.getForGrid")]
        public async Task<Response> GetForGrid(PurchaseOrderParameterEntity purchaseOrderParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await purchaseOrderRepository.SelectForGrid(purchaseOrderParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getListValue", Name = "invoice.purchaseOrder.getListValue")]
        public async Task<Response> GetForList(PurchaseOrderParameterEntity purchaseOrderParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await purchaseOrderRepository.SelectForList(purchaseOrderParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getLovValue", Name = "invoice.purchaseOrder.getLovValue")]
        public async Task<Response> GetForLOV(PurchaseOrderParameterEntity purchaseOrderParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await purchaseOrderRepository.SelectForLOV(purchaseOrderParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }



        [HttpGet]
        [Route("getRecord/{Id:int}", Name = "invoice.purchaseOrder.getRecord")]
        public async Task<Response> GetForRecord(int Id)
        {
            Response response;
            try
            {
                response = new Response(await purchaseOrderRepository.SelectForRecord(Id));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("insert", Name = "invoice.purchaseOrder.insert")]
        public async Task<Response> Insert(PurchaseOrderEntity purchaseOrderEntity)
        {
            Response response;
            try
            {
                int result = await purchaseOrderRepository.Insert(purchaseOrderEntity);
                response = new Response(result);
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("update", Name = "admin.purchaseOrder.update")]
        public async Task<Response> Update(PurchaseOrderEntity purchaseOrderEntity)
        {
            Response response;
            try
            {
                int result = await purchaseOrderRepository.Update(purchaseOrderEntity);
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
