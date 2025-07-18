namespace ECommerce.Entity.Client.Home
{
    public class CategoryEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public int ParentId { get; set; } = 0;
        public string ImageUrl { get; set; } = string.Empty;
    }
    public class CategoryGridEntity
    {
        public List<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();
    }


    public class CategoryParemeterEntity
    {
        public int Id { get; set; } = 0;
        public string Name { set; get; } = string.Empty;
        public int ParentId { get; set; } = 0;
    }
}