using DocumentDBClient;
using ECommerce.Entity.Admin.Master;
using Newtonsoft.Json;

namespace ECommerce.Entity.Client.Search
{
    public class PropertyMainEntity
    {
        public int PropertyId { get; set; } = 0;
        public string PropertyName { get; set; } = string.Empty;
    }

    public class PropertyEntity
    {
        public int PropertyId { get; set; } = 0;
        public string Value { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;


    }
    public class SearchGridEntity
    {
        public List<PropertyMainEntity> Properties { get; set; } = new List<PropertyMainEntity>();
        public List<PropertyEntity> Values { get; set; } = new List<PropertyEntity>();

    }

    public class SearchProductParameterEntity
    {
        public int CategoryId { get; set; } = 0;
        public List<SearchPropertyParameterEntity> SearchProperties { get; set; } = new List<SearchPropertyParameterEntity>();
    }
    public class SearchPropertyParameterEntity
    {
        public int PropertyId { get; set; } = 0;
        public string PropertyName { get; set; } = string.Empty;
        public List<string> Values { get; set; } = new List<string>(); // Only store values, not PropertyName
    }
    public class SearchPropertyEntity
    {
        public string Id { get; set; } = string.Empty;
        public string MediaThumbUrl { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double FinalSellPrice { get; set; } = 0;
    }
    
    public class SearchProductMongoEntity : IBaseEntity
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

        [JsonProperty(PropertyName = "groupProductId")]
        public int GroupProductId { get; set; } = 0;

        [JsonProperty(PropertyName = "groupProductName")]
        public string GroupProductName { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "mediaThumbUrl")]
        public string ThumbUrl { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "properties")]
        public dynamic? Properties { get; set; }

    }

}
