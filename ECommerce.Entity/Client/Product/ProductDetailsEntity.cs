using DocumentDBClient;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace ECommerce.Entity.Client.Product
{
    public class ProductDetailsEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public double SellPrice { get; set; } = 0;
        public double DiscountPercentage { get; set; } = 0;
        public double FinalSellPrice { get; set; } = 0;
        public string CategoryName { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string ThumbUrl { get; set; } = string.Empty;

    }

    public class ProductDetailsSpecificationEntity
    {
        public string PropertyName { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

    }
    public class ProductVarientEntity
    {
        public int Id { get; set; } = 0;
        public int VariantPropertyId { get; set; } = 0;
        public string VariantPropertyName { get; set; } = string.Empty;
        public string VariantPropertyValue { get; set; }  = string.Empty;
        public int ProductId { get; set; } = 0;

    }
    public class ProductAllVarientEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public string ProductVariantIds { get; set; } = string.Empty;
        public double FinalSellPrice { get; set; } = 0;
        public string ImageUrl { get; set; } = string.Empty;

    }



    public class ProductDetailsGridEntity
    {
        public List<ProductDetailsEntity> ProductDetails { get; set; } = new List<ProductDetailsEntity>();
        public List<ProductDetailsSpecificationEntity> ProductDetailsSpecification { get; set; } = new List<ProductDetailsSpecificationEntity>();
        public List<ProductVarientEntity> ProductVarient { get; set; } = new List<ProductVarientEntity>();
        public List<ProductAllVarientEntity> ProductAllVarient { get; set; } = new List<ProductAllVarientEntity>();
    }

    public class ProductDetailsPatameterEntity
    {
        public int Id { get; set; } = 0;

    }

    public class ProductMongoEntity : IBaseEntity
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "name")]
        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "description")]
        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "sku")]
        [BsonElement("sku")]
        public string SKU { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "sellPrice")]
        [BsonElement("sellPrice")]
        public double SellPrice { get; set; } = 0;

        [JsonProperty(PropertyName = "discountPercentage")]
        [BsonElement("discountPercentage")]
        public double DiscountPercentage { get; set; } = 0;

        [JsonProperty(PropertyName = "finalSellPrice")]
        [BsonElement("finalSellPrice")]
        public double FinalSellPrice { get; set; } = 0;

        [JsonProperty(PropertyName = "categoryName")]
        [BsonElement("categoryName")]
        public string CategoryName { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "url")]
        [BsonElement("url")]
        public string Url { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "thumbUrl")]
        [BsonElement("thumbUrl")]
        public string ThumbUrl { get; set; } = string.Empty;

        public List<ProductDetailsSpecificationEntity> Specifications { get; set; } = new List<ProductDetailsSpecificationEntity>();
        public List<ProductVarientEntity> Varients { get; set; } = new List<ProductVarientEntity>();

    }
}
