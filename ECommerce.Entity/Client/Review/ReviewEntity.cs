using DocumentDBClient;
using DocumentFormat.OpenXml.Wordprocessing;
using ECommerce.Entity.Client.Wishlist;
using Irony.Parsing;
using MongoDB.Bson;
using Newtonsoft.Json;
using SixLabors.Fonts;

namespace ECommerce.Entity.Client.Review
{
    public class ReviewEntity
    {
        public int Id { get; set; } = 0;
        public long UserId { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public decimal Rating { get; set; } = decimal.Zero;
        public string Comments { get; set; } = string.Empty;
        public DateTime Date {  get; set; } = DateTime.UtcNow;

        public List<ReviewMediaEntity> MediaList { get; set; } = new List<ReviewMediaEntity>();


        public ReviewMongoEntity ToMongoEntity()
        {
            return new ReviewMongoEntity
            {
                Id = Id > 0 ? Id.ToString() : ObjectId.GenerateNewId().ToString(),
                UserId = UserId,
                ProductId = ProductId,
                Rating = Rating,
                Comments = Comments,
                Date = Date,
                MediaLists = MediaList?.Select(m => new ReviewMediaMongoEntity
                {
                    MediaType = m.MediaType,
                    MediaURL = m.MediaURL
                }).ToList() ?? new List<ReviewMediaMongoEntity>()  // Handle null MediaList
            };
        }
    }

    public class ReviewMediaEntity
    {
        public int Id { get; set; } = 0;
        public int ReviewId { get; set; } = 0;
        public string MediaType { get; set; } = string.Empty;
        public string MediaURL { get; set; } = string.Empty;
    }

    public class ReviewGridEntity
    {
        public List<ReviewEntity> Reviews { get; set; } = new List<ReviewEntity>();
        public List<ReviewMediaEntity> MediaList { get; set; } = new List<ReviewMediaEntity>();
        public int TotalRecords { get; set; }

    }

    public class ReviewParameterEntity : PagingSortingEntity
    {
        public int ProductId { get; set; } = 0;
        public long UserId { get; set; } = 0;

    }

    public class ReviewMongoEntity : IBaseEntity
    {
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "userId")]
        public long UserId { get; set; } = 0;

        [JsonProperty(PropertyName = "productId")]
        public int ProductId { get; set; } = 0;

        [JsonProperty(PropertyName = "rating")]
        public decimal Rating { get; set; } = decimal.Zero;

        [JsonProperty(PropertyName = "comments")]
        public string Comments { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [JsonProperty(PropertyName = "mediaList")]
        public List<ReviewMediaMongoEntity> MediaLists { get; set; } = new List<ReviewMediaMongoEntity>();
    }

    public class ReviewMediaMongoEntity
    {

        [JsonProperty(PropertyName = "reviewId")]
        public int ReviewId { get; set; } = 0;

        [JsonProperty(PropertyName = "mediaType")]
        public string MediaType { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "mediaURL")]
        public string MediaURL { get; set; } = string.Empty;
    }
}
