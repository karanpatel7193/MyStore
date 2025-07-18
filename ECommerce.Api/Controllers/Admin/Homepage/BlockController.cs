using CommonLibrary;
using ECommerce.Api.Common;
using ECommerce.Entity.Admin.Homepage;
using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Common;
using ECommerce.Repository;
using ECommerce.Repository.Admin.Homepage;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers.Admin.Master
{
    [Route("admin/block")]
    [ApiController]
    public class BlockController : ControllerBase, IPageController<BlockEntity, BlockParameterEntity, int>
    {
        IBlockRepository blockRepository;

        public BlockController(IBlockRepository blockRepository)
        {
            this.blockRepository = blockRepository;
        }

        [HttpPost]
        [Route("getForGrid", Name = "admin.block.getForGrid")]
        public async Task<Response> GetForGrid(BlockParameterEntity blockParemeterEntity)
        {
            Response response;
            try
            {
                response = new Response(await blockRepository.SelectForGrid(blockParemeterEntity));

            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpGet]
        [Route("getRecord/{Id:int}", Name = "admin.block.getRecord")]
        public async Task<Response> GetForRecord(int Id)
        {
            Response response;
            try
            {
                response = new Response(await blockRepository.SelectForRecord(Id));

            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("insert", Name = "admin.block.insert")]
        public async Task<Response> Insert(BlockEntity blockEntity)
        {
            Response response;
            try
            {
                response = new Response(await blockRepository.Insert(blockEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }


        [HttpPost]
        [Route("update", Name = "admin.block.update")]
        public async Task<Response> Update(BlockEntity blockEntity)
        {
            Response response;
            try
            {
                int id = await blockRepository.Update(blockEntity);
                response = new Response(id);
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("delete/{Id:int}", Name = "admin.block.delete")]
        public async Task<Response> Delete(int Id)
        {
            Response response;
            try
            {
                await blockRepository.Delete(Id);
                response = new Response();
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getAddMode", Name = "admin.block.getAddMode")]
        public async Task<Response> GetForAdd(BlockParameterEntity blockParemeterEntity)
        {
            Response response;
            try
            {
                response = new Response(await blockRepository.SelectForAdd(blockParemeterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getEditMode", Name = "admin.block.getEditMode")]
        public async Task<Response> GetForEdit(BlockParameterEntity blockParemeterEntity)
        {
            Response response;
            try
            {
                BlockEditEntity blockEditEntity = new BlockEditEntity();
                blockEditEntity = await blockRepository.SelectForEdit(blockParemeterEntity);
                response = new Response(blockEditEntity);

            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getLovValue", Name = "admin.block.getLovValue")]
        public async Task<Response> GetForLOV(BlockParameterEntity blockParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await blockRepository.SelectForLOV(blockParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getListValue", Name = "admin.block.getListValue")]
        public async Task<Response> GetForList(BlockParameterEntity blockParemeterEntity)
        {
            Response response;
            try
            {
                response = new Response(await blockRepository.SelectForList(blockParemeterEntity));

            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
    }
}
