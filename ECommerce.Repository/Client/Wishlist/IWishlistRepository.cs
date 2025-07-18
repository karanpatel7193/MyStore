using ECommerce.Entity.Client.Wishlist;

namespace ECommerce.Repository.Client.Wishlist
{
    public  interface IWishlistRepository:IBusiness<WishlistEntity, WishlistMainEntity, WishlistAddEntity, WishlistEditEntity, WishlistGridEntity, WishlistListEntity, WishlistParameterEntity, int>
    {
        public Task DeleteWishlist(WishlistParameterEntity wishlistParameterEntity);
        public Task CheckIfProductInWishlist(WishlistParameterEntity wishlistParameterEntity);

        public Task<int> InsertBulk(WishlistMainEntity wishlistMainEntity);

    }
}
