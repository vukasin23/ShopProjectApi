using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

        public Coupon? Coupon { get; set; }
        public int? CouponId { get; set; }

        public ShippingMethod ShippingMethod { get; set; }

        public int ShippingMethodId { get; set; }

        public Address Address { get; set; }

        public int AddressId { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }
        
        public decimal TotalPrice { get; set; }

    }
}
