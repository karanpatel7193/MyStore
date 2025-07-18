using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Entity.Client.Cart
{
    public class CartDiscountCouponEntity
    {
        public int Id { get; set; } = 0;
        public int CartId { get; set; } = 0;
        public string CouponDiscountCode { get; set; } = string.Empty;

        // Navigation Property for Cart
        public CartEntity Cart { get; set; } 
    }

}
