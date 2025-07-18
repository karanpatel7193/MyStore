using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entity.Client.Home
{
    public class CategoryPropertyValueEntity
    {
        public int CategoryId { get; set; } = 0;
        public int PropertyId { get; set; } = 0;
        public string Value { get; set; } = string.Empty;
        public string Unit { get; set; } = String.Empty;
    }
}
