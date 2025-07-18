using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entity.Client.Home
{
    public class BlockProductEntity
    {
        public int BlockId { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public string ProductName { get; set; } = string.Empty;
        public string ThumbUrl { get; set; } = string.Empty;
        public double FinalSellPrice { get; set; } = 0;
        public double DiscountPercentage { get; set; } = 0;
        public double DiscountAmount { get; set; } = 0;

    }
}
