using ECommerce.Entity.Admin.Master;

namespace ECommerce.Entity.Client.Wishlist
{
    public class WishlistMainEntity
    {
        public long UserId { get; set; } = 0;
        public List<WishlistEntity> Wishlist { get; set; } = new List<WishlistEntity>();
    }
    public class WishlistEntity
    {
        public int Id { get; set; } = 0;
        public long UserId { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
    public class WishlistProductEntity
    {
        public int Id { get; set; } = 0;
        public long UserId { get; set; } = 0;
        public long ProductId { get; set; } = 0;
        public string MediaThumbUrl { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double FinalSellPrice { get; set; } = 0;
        public bool IsExpiry { get; set; } = false;

    }

    public class WishlistGridEntity
    {
        public List<WishlistProductEntity> Wishlists { get; set; } = new List<WishlistProductEntity>();
        public int TotalRecords { get; set; }

    }
    public class WishlistAddEntity
    {
    }

    public class WishlistEditEntity
    {
    }

    public class WishlistListEntity
    {
    }

    public class WishlistParameterEntity:PagingSortingEntity
    {
        public int ProductId { get; set; } = 0;
        public long UserId { get; set; } = 0;

    }

}
