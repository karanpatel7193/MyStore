using DocumentDBClient;
using ECommerce.Entity.Common;
using Newtonsoft.Json;

namespace ECommerce.Entity.Admin.Master
{
    public class ProductEntity : ProductInventoryEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double FinalSellPrice { get; set; } = 0;
        public string LongDescription { get; set; } = string.Empty;
        public int CategoryId { get; set; } = 0;
        public string CategoryName { get; set; } = string.Empty;
        public bool AllowReturn { get; set; } = false;
        public string ReturnPolicy { get; set; } = string.Empty;
        public bool IsExpiry { get; set; } = false;
        public string ThumbUrl { get; set; } = string.Empty;
        public long CreatedBy { get; set; } = 0;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public long LastUpdatedBy { get; set; } = 0;
        public DateTime LastUpdatedOn { get; set; } = DateTime.UtcNow;
        public string SKU { get; set; } = string.Empty;
        public int UPC { get; set; } = 0;
        public int ParentProductId { get; set; } = 0;
        public dynamic? Properties { get; set; }  
        public List<ProductMediaEntity> ProductMedias { get; set; } = new List<ProductMediaEntity>();

        public List<ProductVariantEntity> VariantCombinations { get; set; } = new List<ProductVariantEntity>();

        public List<ProductEntity> Variants { get; set; } = new List<ProductEntity>();

        public string ProductVariantIds { get; set; } = string.Empty;
        public List<int> ProductVariantIndexs { get; set; } = new List<int>();

        public ProductMongoEntity ToMongoEntity()
        {
            ProductMongoEntity productMongoEntity = new ProductMongoEntity
            {
                Id = Id.ToString(),
                Name = Name,
                Description = Description,
                FinalSellPrice = FinalSellPrice,
                LongDescription = LongDescription,
                CategoryId = CategoryId,
                CategoryName = CategoryName,
                ThumbUrl = ThumbUrl,
                Properties = Properties

            };
            return productMongoEntity;
        }
    }

    public class ProductInventoryEntity
    {
        public int Id { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public int OpeningQty { get; set; } = 0;
        public int BuyQty { get; set; } = 0;
        public int LockQty { get; set; } = 0;
        public int OrderQty { get; set; } = 0;
        public int SellQty { get; set; } = 0;
        public int ClosingQty { get; set; } = 0;
        public double CostPrice { get; set; } = 0;
        public double SellPrice { get; set; } = 0;
        public double DiscountPercentage { get; set; } = 0.0;
        public double DiscountAmount { get; set; } = 0;
        public double FinalSellPrice { get; set; } = 0;
    }

    public class ProductMediaEntity
    {
        public int Id { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public int Type { get; set; } = 0;
        public string Url { get; set; } = string.Empty;
        public string ThumbUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public FileEntity File { get; set; } = null;
    }

    public class ProductMainEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
    }
    public class ProductAddEntity
    {
        public List<CategoryMainEntity> Categories { get; set; } = new List<CategoryMainEntity>();
        //public List<GroupProductMainEntity> GroupProducts { get; set; } = new List<GroupProductMainEntity>();
    }

    public class ProductEditEntity : ProductAddEntity
    {
        public ProductEntity Product { get; set; } = new ProductEntity();
        public List<ProductVariantEntity> VariantCombinations { get; set; } = new List<ProductVariantEntity>();
        public List<ProductEntity> Variants { get; set; } = new List<ProductEntity>();
    }

    public class ProductListEntity : ProductGridEntity
    {
        public List<CategoryMainEntity> Categories { get; set; } = new List<CategoryMainEntity>();
        //public List<GroupProductMainEntity> GroupProducts { get; set; } = new List<GroupProductMainEntity>();
    }
    public class ProductGridEntity
    {
        public List<ProductEntity> Products { get; set; } = new List<ProductEntity>();
        public int TotalRecords { get; set; }
    }
    public class ProductParameterEntity : PagingSortingEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; } = 0;

    }

    public class ProductMongoEntity : IBaseEntity
    {
        public string Id { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "FinalSellPrice")]
        public double FinalSellPrice { get; set; } = 0;

        [JsonProperty(PropertyName = "longDescription")]
        public string LongDescription { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "categoryId")]
        public int CategoryId { get; set; } = 0;

        [JsonProperty(PropertyName = "categoryName")]
        public string CategoryName { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "mediaThumbUrl")]
        public string ThumbUrl { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "properties")]
        public dynamic? Properties { get; set; }

    }

}
