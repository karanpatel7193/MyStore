namespace ECommerce.Entity.Client.Home
{
    public class BlockEntity 
    { 
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class BlockGridEntity
    {
        public List<BlockEntity> Blocks { get; set; } = new List<BlockEntity>();
        public List<BlockProductEntity> Products { get; set; } = new List<BlockProductEntity>();
    }
   
}
