using CommonLibrary;
using ECommerce.Api.Common;
using ECommerce.Repository;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Entity.Client.Wishlist;
using ECommerce.Repository.Client.Wishlist;
using ECommerce.Entity.Client.Cart;

namespace ECommerce.Api.Controllers.Client.Wishlist
{
    [Route("client/wishlist")]
    [ApiController]
    public class WishlistController : ControllerBase, IPageController<WishlistEntity, WishlistParameterEntity, int>
    {
        IWishlistRepository wishlistRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public WishlistController(IWebHostEnvironment hostingEnvironment, IWishlistRepository wishlistRepository)
        {
            this.wishlistRepository = wishlistRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [Route("getForGrid", Name = "client.wishlist.getForGrid")]
        //[AuthorizeAPI(pageName: "WishlistList", pageAccess: PageAccessValues.View)]

        public async Task<Response> GetForGrid(WishlistParameterEntity wishlistParemeterEntity)
        {
            Response response;
            try
            {
                response = new Response(await wishlistRepository.SelectForGrid(wishlistParemeterEntity));

            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("insert", Name = "client.wishlist.insert")]
        //[AuthorizeAPI(pageName: "WishlistList", pageAccess: PageAccessValues.Insert)]

        public async Task<Response> Insert(WishlistEntity wishlistEntity)
        {
            Response response;
            try
            {
                response = new Response(await wishlistRepository.Insert(wishlistEntity));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }

        [HttpPost]
        [Route("insertBulk", Name = "client.wishlist.insertBulk")]
        //[AuthorizeAPI(pageName: "User", pageAccess: PageAccessValues.Insert)]
        public async Task<Response> InsertBulk(WishlistMainEntity wishlistMainEntity)
        {
            Response response;
            try
            {
                response = new Response(await wishlistRepository.InsertBulk(wishlistMainEntity ));
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }


        [HttpPost]
        [Route("deleteWishlist", Name = "client.wishlist.deleteWishlist")]
        //[AuthorizeAPI(pageName: "WishlistList", pageAccess: PageAccessValues.Delete)]

        public async Task<Response> Delete(WishlistParameterEntity wishlistParameterEntity)
        {
            Response response;
            try
            {
                await wishlistRepository.DeleteWishlist(wishlistParameterEntity);
                response = new Response();
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
        [HttpPost]
        [Route("CheckIfProductInWishlist", Name = "client.wishlist.CheckIfProductInWishlist")]
        //[AuthorizeAPI(pageName: "WishlistList", pageAccess: PageAccessValues.Delete)]

        public async Task<Response> CheckIfProductInWishlist(WishlistParameterEntity wishlistParameterEntity)
        {
            Response response;
            try
            {
                await wishlistRepository.CheckIfProductInWishlist(wishlistParameterEntity);
                response = new Response();
            }
            catch (Exception ex)
            {
                response = new Response(await ex.WriteLogFileAsync(), ex);
            }
            return response;
        }
        [HttpPost]
        [Route("GetForAdd", Name = "client.wishlist.GetForAdd")]
        public Task<Response> GetForAdd(WishlistParameterEntity objParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        [Route("GetForEdit", Name = "client.wishlist.GetForEdit")]
        public Task<Response> GetForEdit(WishlistParameterEntity objParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        [Route("GetForList", Name = "client.wishlist.GetForList")]
        public Task<Response> GetForList(WishlistParameterEntity objParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        [Route("GetForRecord", Name = "client.wishlist.GetForRecord")]
        public Task<Response> GetForRecord(int objPK)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        [Route("GetForLOV", Name = "client.wishlist.GetForLOV")]
        public Task<Response> GetForLOV(WishlistParameterEntity objParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        [Route("Update", Name = "client.wishlist.Update")]
        public Task<Response> Update(WishlistEntity objEntity)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Delete", Name = "client.wishlist.Delete")]
        public Task<Response> Delete(int objPK)
        {
            throw new NotImplementedException();
        }
    }
}
