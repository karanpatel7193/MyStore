using CommonLibrary;
using ECommerce.Api.Common;
using ECommerce.Business.Admin.Master;
using ECommerce.Entity.Admin.Master;
using ECommerce.Entity.Common;
using ECommerce.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Imaging;

namespace ECommerce.Api.Controllers.Admin.Master
{
    [Route("admin/product")]
    [ApiController]
    public class ProductController : ControllerBase, IPageController<ProductEntity, ProductParameterEntity, int>
    {
        IProductRepositoroy productRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductController(IWebHostEnvironment hostingEnvironment, IProductRepositoroy productRepository)
        {
            this.productRepository = productRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [Route("getForGrid", Name = "admin.product.getForGrid")]
        [AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.View)]

        public async Task<Response> GetForGrid(ProductParameterEntity productParemeterEntity)
        {
            Response response;
            try
            {
                response = new Response(await productRepository.SelectForGrid(productParemeterEntity));

            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpGet]
        [Route("getRecord/{Id:int}", Name = "admin.product.getRecord")]
        [AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.View)]

        public async Task<Response> GetForRecord(int Id)
        {
            Response response;
            try
            {
                response = new Response(await productRepository.SelectForRecord(Id));

            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("insert", Name = "admin.product.insert")]
        [AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.Insert)]

        public async Task<Response> Insert(ProductEntity productEntity)
        {
            Response response;
            try
            {
                productEntity.CreatedBy = AuthenticateCliam.UserId(Request);
                productEntity.CreatedOn = DateTime.UtcNow;

                MediaPreProcess(productEntity);

                int id = await productRepository.Insert(productEntity);

                MediaPostProcess(id, productEntity);

                response = new Response(id);
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }


        [HttpPost]
        [Route("update", Name = "admin.product.update")]
        [AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.Update)]

        public async Task<Response> Update(ProductEntity productEntity)
        {
            Response response;
            try
            {
                productEntity.LastUpdatedBy = AuthenticateCliam.UserId(Request);
                productEntity.LastUpdatedOn = DateTime.UtcNow;

                MediaPreProcess(productEntity);

                int id = await productRepository.Update(productEntity);

                MediaPostProcess(id, productEntity);

                response = new Response(id);
                //response = new Response(await productRepository.Update(productEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("delete/{Id:int}", Name = "admin.product.delete")]
        [AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.Delete)]

        public async Task<Response> Delete(int Id)
        {
            Response response;
            try
            {
                await productRepository.Delete(Id);
                response = new Response();
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getAddMode", Name = "admin.product.getAddMode")]
        [AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.Insert)]

        public async Task<Response> GetForAdd(ProductParameterEntity productParemeterEntity)
        {
            Response response;
            try
            {
                response = new Response(await productRepository.SelectForAdd(productParemeterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getEditMode", Name = "admin.product.getEditMode")]
       [AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.Update)]

        public async Task<Response> GetForEdit(ProductParameterEntity productParemeterEntity)
        {
            Response response;
            try
            {
                //response = new Response(await productRepository.SelectForEdit(productParemeterEntity));
                ProductEditEntity productEditEntity = new ProductEditEntity();
                productEditEntity = await productRepository.SelectForEdit(productParemeterEntity);
                MyFile file = new MyFile();
                for (int i = 0; i < productEditEntity.Product.ProductMedias.Count; i++)
                {
                    file.Path = _hostingEnvironment.ContentRootPath + productEditEntity.Product.ProductMedias[i].Url;

                    var content = await file.ReadBase64StringAsync();
                    productEditEntity.Product.ProductMedias[i].File = new FileEntity
                    {
                        Name = productEditEntity.Product.Id + ".jpg",
                        Content = "data:image/jpeg;base64," + content,
                        Path = file.Path,
                        Extention = productEditEntity.Product.ProductMedias[i].Url.Split('.').LastOrDefault()
                    };
                }

                response = new Response(productEditEntity);

            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getLovValue", Name = "admin.product.getLovValue")]
        [AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.IgnoreAuthorization)]

        public async Task<Response> GetForLOV(ProductParameterEntity productParameterEntity)
        {
            Response response;
            try
            {
                response = new Response(await productRepository.SelectForLOV(productParameterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("getListValue", Name = "admin.product.getListValue")]
        [AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.View)]

        public async Task<Response> GetForList(ProductParameterEntity productParemeterEntity)
        {
            Response response;
            try
            {
                response = new Response(await productRepository.SelectForList(productParemeterEntity));

            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }


        [HttpPost]
        [Route("getForPropertyGrid", Name = "admin.product.getForPropertyGrid")]
        [AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.View)]

        public async Task<Response> GetForPropertyGrid(ProductPropertyParameterEntity productPropertyParemeterEntity)
        {
            Response response;
            try
            {
                response = new Response(await productRepository.SelectForPropertyGrid(productPropertyParemeterEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("insertProperty", Name = "admin.product.insertProperty")]
        [AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.Insert)]

        public async Task<Response> InsertProperty(ProductPropertyParameterEntity productPropertyParemeterEntity)
        {
            Response response;
            try
            {
                await productRepository.InsertProperty(productPropertyParemeterEntity);
                response = new Response();
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("sync", Name = "admin.product.sync")]
        public async Task<Response> Sync(ProductParameterEntity productParameterEntity)
        {
            Response response;
            try
            {
                await productRepository.Sync(productParameterEntity);
                response = new Response();
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;

        }

        [HttpPost]
        [Route("syncAll", Name = "admin.product.syncAll")]
        public async Task<Response> SyncAll()
        {
            Response response;
            try
            {

                await productRepository.SyncAll();
                response = new Response();
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;

        }

        #region Private methods
        private void MediaPreProcess(ProductEntity productEntity)
        {
            if (productEntity.ProductMedias != null && productEntity.ProductMedias.Count != 0)
            {
                for (int i = 0; i < productEntity.ProductMedias.Count; i++)
                {
                    if (productEntity.ProductMedias[i].Type == ProductMediaType.Image.GetHashCode())
                        productEntity.ProductMedias[i].Url = Common.AppSettings.PathDocumentUpload + "/Product/{Id}/" + i + ".jpg";
                    else
                        productEntity.ProductMedias[i].Url = Common.AppSettings.PathDocumentUpload + "/Product/{Id}/" + i + ".mp4";
                    productEntity.ProductMedias[i].ThumbUrl = Common.AppSettings.PathDocumentUpload + "/Product/{Id}/" + i + "_thumb.jpg";
                }
            }
        }
        private void MediaPostProcess(int id, ProductEntity productEntity)
        {
            if (id > 0 && productEntity.ProductMedias != null && productEntity.ProductMedias.Count != 0)
            {
                for (int i = 0; i < productEntity.ProductMedias.Count; i++)
                {
                    if (productEntity.ProductMedias[i].Type == ProductMediaType.Image.GetHashCode())
                    {
                        MyFile file = new MyFile
                        {
                            Path = _hostingEnvironment.ContentRootPath + Common.AppSettings.PathDocumentUpload + "\\Product\\" + id + "\\",
                            Name = i + ".jpg"
                        };

                        if (file.Exists())
                            file.Delete();

                        file.Name = i + "_thumb.jpg";
                        if (file.Exists())
                            file.Delete();

                        file.Name = i + "_Temp.jpg";
                        if (file.Exists())
                            file.Delete();

                        file.Content = productEntity.ProductMedias[i].File.Content;
                        file.Create();

                        // Create square image
                        MyImage image = new MyImage();
                        image.CreateImage(file.Path + file.Name, file.Path + i + "_Temp2.jpg", ImageFormat.Jpeg);
                        image.CreateSquareImage(file.Path + i + "_Temp2.jpg", file.Path + i + ".jpg", Common.AppSettings.ProductImageSize, false);
                        image.CreateSquareImage(file.Path + i + "_Temp2.jpg", file.Path + i + "_thumb.jpg", Common.AppSettings.ProductThumbImageSize);
                    }
                    else
                    {
                        MyFile file = new MyFile
                        {
                            Path = _hostingEnvironment.ContentRootPath + Common.AppSettings.PathDocumentUpload + "\\Product\\" + id + "\\",
                            Name = i + ".mp4"
                        };

                        if (file.Exists())
                            file.Delete();

                        file.Name = i + "_thumb.jpg";
                        if (file.Exists())
                            file.Delete();

                        file.Content = productEntity.ProductMedias[i].File.Content;
                        file.Create();

                        //TODO: Create thumb image of video 1st frame.
                    }
                }

            }
        }

        #endregion
    }
}
