using CommonLibrary;
using ECommerce.Api.Common;
using ECommerce.Entity.Admin.Master;
using ECommerce.Repository;
using ECommerce.Repository.Admin.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Admin.Master
{
    [Route("admin/property")]
    [ApiController]
    public class PropertyController : ControllerBase, IPageControllerPartial<PropertyEntity, PropertyParameterEntity, int>
    {
        IPropertyRepository PropertyRepository;

        public PropertyController(IPropertyRepository PropertyRepository)
        {
            this.PropertyRepository = PropertyRepository;
        }

        #region Interface public methods
        /// <summary>
        /// Get all columns values for perticular Property record.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getRecord/{Id:int}", Name = "admin.property.record")]
        [AuthorizeAPI(pageName: "Property", pageAccess: PageAccessValues.View)]
        public async Task<Response> GetForRecord(int Id)
        {
            Response response;
            try
            {
                response = new Response(await PropertyRepository.SelectForRecord(Id));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        /// <summary>
        /// Get main columns informations for bind Property LOV
        /// </summary>
        /// <param name="PropertyParameterEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getLovValue", Name = "admin.property.lovValue")]
        [AuthorizeAPI(pageName: "Property", pageAccess: PageAccessValues.IgnoreAuthorization)]
        public async Task<Response> GetForLOV(PropertyParameterEntity propertyParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await PropertyRepository.SelectForLOV(propertyParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        /// <summary>
        /// Get Property list for bind grid.
        /// </summary>
        /// <param name="PropertyParameterEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getGridData", Name = "admin.property.gridData")]
        [AuthorizeAPI(pageName: "Property", pageAccess: PageAccessValues.View)]
        public async Task<Response> GetForGrid(PropertyParameterEntity propertyParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await PropertyRepository.SelectForGrid(propertyParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        /// <summary>
        /// Insert record in Property table.
        /// </summary>
        /// <param name="PropertyEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("insert", Name = "admin.property.insert")]
        [AuthorizeAPI(pageName: "Property", pageAccess: PageAccessValues.Insert)]
        public async Task<Response> Insert(PropertyEntity PropertyEntity)
        {
            Response response;
            try
            {
                response = new Response(await PropertyRepository.Insert(PropertyEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        /// <summary>
        /// Update record in Property table.
        /// </summary>
        /// <param name="PropertyEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update", Name = "admin.property.update")]
        [AuthorizeAPI(pageName: "Property", pageAccess: PageAccessValues.Update)]
        public async Task<Response> Update(PropertyEntity PropertyEntity)
        {
            Response response;
            try
            {

                response = new Response(await PropertyRepository.Update(PropertyEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        /// <summary>
        /// Delete record from Property table.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("delete/{Id:int}", Name = "admin.property.delete")]
        [AuthorizeAPI(pageName: "Property", pageAccess: PageAccessValues.Delete)]
        public async Task<Response> Delete(int Id)
        {
            Response response;
            try
            {
                await PropertyRepository.Delete(Id);
                response = new Response();
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
        #endregion
    }
}
