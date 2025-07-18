using CommonLibrary;
using DocumentFormat.OpenXml.Office2010.Excel;
using ECommerce.Api.Common;
using ECommerce.Business.Admin.Master;
using ECommerce.Business.Client.Home;
using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Common;
using ECommerce.Repository;
using ECommerce.Repository.Admin.Master;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SharpCompress.Common;
using System.Drawing.Imaging;
using static ECommerce.Entity.Admin.Master.CategoryEntity;

namespace ECommerce.Api.Controllers.Admin.Master
{
    [Route("admin/category")]
    [ApiController]
    public class CategoryController : ControllerBase, IPageController<CategoryEntity, CategoryParemeterEntity, int>
    {
        ICategoryRepository categoryRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CategoryController(IWebHostEnvironment hostingEnvironment, ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [Route("getForGrid", Name = "admin.category.getForGrid")]
        public async Task<Response> GetForGrid(CategoryParemeterEntity categoryParemeterEntity)
        {
            Response response;
            try
            {
                response = new Response(await categoryRepository.SelectForGrid(categoryParemeterEntity));

            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpGet]
        [Route("getRecord/{Id:int}", Name = "admin.category.getRecord")]
        public async Task<Response> GetForRecord(int Id)
        {
            Response response;
            try
            {
                response = new Response(await categoryRepository.SelectForRecord(Id));

            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }


        [HttpPost]
        [Route("insert", Name = "admin.category.insert")]
        public async Task<Response> Insert(CategoryEntity categoryEntity)
        {
            Response response;
            try
            {
                PreProcess(categoryEntity);

                int id = await categoryRepository.Insert(categoryEntity);

                if (id > 0)
                {
                    await PostProcess(id, categoryEntity);
                }

                response = new Response(id);
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("update", Name = "admin.category.update")]
        public async Task<Response> Update(CategoryEntity categoryEntity)
        {
            Response response;
            try
            {
                PreProcess(categoryEntity);

                int id = await categoryRepository.Update(categoryEntity);

                //if (id > 0)
                if (id > 0 && categoryEntity.File != null && categoryEntity.File.Name != null)
                {
                    await PostProcess(id, categoryEntity);
                }
                response = new Response(id);
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("delete/{Id:int}", Name = "admin.category.delete")]
        public async Task<Response> Delete(int Id)
        {
            Response response;
            try
            {
                await categoryRepository.Delete(Id);

                MyFile File = new MyFile();
                File.Path = _hostingEnvironment.ContentRootPath + Common.AppSettings.PathDocumentUpload + "\\Category\\";
                File.Name = Id + ".jpg";
                if (File.Exists())
                    File.Delete();

                File.Name = Id + "_Temp.jpg";
                if (File.Exists())
                    File.Delete();

                response = new Response();
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpGet]
        [Route("getParent", Name = "admin.category.getParent")]
        public async Task<Response> GetParent()
        {
            Response response;
            try
            {
                response = new Response(await categoryRepository.SelectParent());
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getChild/{ParentId:int}", Name = "admin.category.getChild")]
        public async Task<Response> GetChildByParentId(CategoryParemeterEntity categoryParemeterEntity)
        {
            Response response;
            try
            {
                response = new Response(await categoryRepository.SelectChild(categoryParemeterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getAddMode", Name = "admin.category.addMode")]
        public async Task<Response> GetForAdd(CategoryParemeterEntity categoryParemeterEntity)
        {
            Response response;
            try
            {
                response = new Response(await categoryRepository.SelectForAdd(categoryParemeterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getEditMode", Name = "admin.category.editMode")]
        public async Task<Response> GetForEdit(CategoryParemeterEntity categoryParemeterEntity)
        {
            Response response;
            try
            {
                CategoryEditEntity categoryEditEntity = new CategoryEditEntity();
                categoryEditEntity = await categoryRepository.SelectForEdit(categoryParemeterEntity);

                if (categoryEditEntity.Category.ImageUrl != null)
                {
                    MyFile file = new MyFile();
                    file.Path = _hostingEnvironment.ContentRootPath + categoryEditEntity.Category.ImageUrl;

                    var content = await file.ReadBase64StringAsync();
                    categoryEditEntity.Category.File = new FileEntity
                    {
                        Name = categoryEditEntity.Category.Id + ".jpg",
                        Content = "data:image/jpeg;base64," + content,
                        Path = file.Path,
                        Extention = ".jpg"
                    };
                }

                response = new Response(categoryEditEntity);
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getLovValue", Name = "admin.category.lovValue")]
        public async Task<Response> GetForLOV(CategoryParemeterEntity categoryParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await categoryRepository.SelectForLOV(categoryParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getListValue", Name = "admin.category.listValue")]
        public async Task<Response> GetForList(CategoryParemeterEntity categoryParemeterEntity)
        {
            Response response;
            try
            {
                response = new Response(await categoryRepository.SelectForList(categoryParemeterEntity));

            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }


        private void PreProcess(CategoryEntity categoryEntity)
        {
            if (categoryEntity.File != null && categoryEntity.File.Name != null)
            {
                categoryEntity.ImageUrl = Common.AppSettings.PathDocumentUpload + "/Category/";

                MyFile file = new MyFile();
                file.Path = _hostingEnvironment.ContentRootPath + Common.AppSettings.PathDocumentUpload + "\\Category\\";
                file.Name = categoryEntity.File.Name;

                if (file.Exists())
                {
                    file.Delete();
                }
            }
        }

        private async Task PostProcess(int id, CategoryEntity categoryEntity)
        {
            if (categoryEntity.File != null && categoryEntity.File.Content?.Length > 0)
            {
                MyFile file = new MyFile
                {
                    Path = _hostingEnvironment.ContentRootPath + Common.AppSettings.PathDocumentUpload + "\\Category\\",
                    Name = id + ".jpg"
                };

                MyFile tempFile = new MyFile
                {
                    Path = file.Path,
                    Name = id + "_Temp.jpg"
                };

                if (tempFile.Exists())
                    tempFile.Delete();

                tempFile.Content = categoryEntity.File.Content;
                tempFile.Create();

                MyImage image = new MyImage();
                string tempImagePath = file.Path + id + "_Temp2.jpg";
                string finalImagePath = file.Path + id + ".jpg";

                image.CreateImage(file.Path + tempFile.Name, tempImagePath, ImageFormat.Jpeg);
                image.CreateSquareImage(tempImagePath, finalImagePath, Common.AppSettings.CategoryImageSize);
            }
        }

        [HttpPost]
        [Route("getCategoryPropertyValue", Name = "admin.category.getCategoryPropertyValue")]
        public async Task<Response> GetForCategoryPropertyValue()
        {
            Response response;
            try
            {
                response = new Response(await categoryRepository.SelectCategoryPropertyValue());
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }


        //private async Task PostProcess(int id, CategoryEntity categoryEntity)
        //{
        //    if (categoryEntity.File != null && categoryEntity.File.Name != null)
        //    {
        //        MyFile file = new MyFile();
        //        file.Path = _hostingEnvironment.ContentRootPath + Common.AppSettings.PathDocumentUpload + "\\Category\\";
        //        file.Name = id + ".jpg";

        //        MyFile tempFile = new MyFile();
        //        tempFile.Path = file.Path;
        //        tempFile.Name = id + "_Temp.jpg";
        //        if (tempFile.Exists())
        //            tempFile.Delete();

        //        tempFile.Content = categoryEntity.File.Content;
        //        tempFile.Create();

        //        MyImage image = new MyImage();
        //        image.CreateImage(file.Path + tempFile.Name, file.Path + id + "_Temp2.jpg", ImageFormat.Jpeg);
        //        image.CreateSquareImage(file.Path + id + "_Temp2.jpg", file.Path + id + ".jpg", Common.AppSettings.CategoryImageSize);
        //    }
        //}

    }
}
