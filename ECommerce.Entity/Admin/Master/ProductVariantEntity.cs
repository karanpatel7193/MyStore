using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entity.Admin.Master
{
    public class ProductVariantEntity
    {
        public int Index { get; set; } = 0;
        public int Id { get; set; } = 0;
        public int ProductId { get; set; } = 0;
        public int VariantPropertyId { get; set; } = 0;
        public string VariantPropertyValue { get; set; } = string.Empty;
        public string VariantPropertyName { get; set; } = string.Empty;
    }
    public class ProductVariantGridEntity
    {
        public List<ProductVariantEntity> VariantCombinations { get; set; } = new List<ProductVariantEntity>();

    }
    public class ProductVariantParameterEntity
    {
        public int ProductId { get; set; } = 0;
    }


}
