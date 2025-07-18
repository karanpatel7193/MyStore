using CommonLibrary;
using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Admin.Master.CategoryProperty;
using ECommerce.Repository;
using ECommerce.Repository.Admin.Master;
using ECommerce.Repository.Admin.Master.CategoryProperty;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Admin.Master
{
    [Route("admin/categoryProperty")]
    [ApiController]
    public class CategoryPropertyController : ControllerBase, IPageController<CategoryPropertyEntity, CategoryPropertyParameterEntity, int>
    {
        public readonly ICategoryPropertyRepository categoryPropertyRepository;

        public CategoryPropertyController(ICategoryPropertyRepository categoryPropertyRepository)
        {
            this.categoryPropertyRepository = categoryPropertyRepository;
        }

        [HttpPost]
        [Route("getForGrid", Name = "admin.categoryProperty.getForGrid")]
        public async Task<Response> GetForGrid(CategoryPropertyParameterEntity parameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await categoryPropertyRepository.SelectForGrid(parameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpGet]
        [Route("getRecord/{id:int}", Name = "admin.categoryProperty.getRecord")]
        public async Task<Response> GetForRecord(int id)
        {
            Response response;
            try
            {
                response = new Response(await categoryPropertyRepository.SelectForRecord(id));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
        [HttpPost]
        [Route("getCategoryProperty", Name = "admin.categoryProperty.getCategoryProperty")]
        public async Task<Response> GetForCategoryProperty(CategoryPropertyParameterEntity categoryPropertyParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await categoryPropertyRepository.SelectForCategoryProperty(categoryPropertyParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
        [HttpPost]
        [Route("insert", Name = "admin.categoryProperty.insert")]
        public async Task<Response> Insert(CategoryPropertyEntity categoryPropertyEntity)
        {
            Response response;
            try
            {
                response = new Response(await categoryPropertyRepository.Insert(categoryPropertyEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("update", Name = "admin.categoryProperty.update")]
        public async Task<Response> Update(CategoryPropertyEntity categoryPropertyEntity)
        {
            Response response;
            try
            {
                response = new Response(await categoryPropertyRepository.Update(categoryPropertyEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("delete/{id:int}", Name = "admin.categoryProperty.delete")]
        public async Task<Response> Delete(int id)
        {
            Response response;
            try
            {
                await categoryPropertyRepository.Delete(id);
                response = new Response();
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getForAdd", Name = "admin.categoryProperty.getForAdd")]
        public async Task<Response> GetForAdd(CategoryPropertyParameterEntity categoryPropertyParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await categoryPropertyRepository.SelectForAdd(categoryPropertyParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getForEdit", Name = "admin.categoryProperty.getForEdit")]
        public async Task<Response> GetForEdit(CategoryPropertyParameterEntity categoryPropertyParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await categoryPropertyRepository.SelectForEdit(categoryPropertyParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getForList", Name = "admin.categoryProperty.getForList")]
        public async Task<Response> GetForList(CategoryPropertyParameterEntity categoryPropertyParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await categoryPropertyRepository.SelectForList(categoryPropertyParameterEntity));

            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getForLOV", Name = "admin.categoryProperty.getForLOV")]
        public async Task<Response> GetForLOV(CategoryPropertyParameterEntity categoryPropertyParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await categoryPropertyRepository.SelectForLOV(categoryPropertyParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
    }
}
