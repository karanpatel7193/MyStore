namespace ECommerce.Entity.Client.Cart
{
    public class CartMainEntity
    {
        public long UserId { get; set; }
        public List<CartEntity> CartItems { get; set; } = new List<CartEntity>();
    }
    public class CartEntity
    {
        public int Id { get; set; } = 0;
        public long UserId { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public double DiscountAmount { get; set; } = 0;
        public decimal OfferPrice { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
        public string ProductName { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string MediaThumbUrl { get; set; } = string.Empty;
    }
    public class CartGridEntity
    {
        public List<CartEntity> Products { get; set; } = new List<CartEntity>();
        public int TotalRecords { get; set; }
    }
    public class CartParameterEntity : PagingSortingEntity
    {
        public long UserId { get; set; } = 0;
        public int ProductId { get; set; } = 0;

        public string ProductName { get; set; } = string.Empty;
    }
}
