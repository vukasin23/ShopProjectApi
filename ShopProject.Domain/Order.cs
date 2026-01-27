using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public virtual ICollection<OrderLine> OrderLines { get; set; }
        public decimal TotalAmount { get; set; }

        public Coupon Coupon { get; set; }
        public int CouponId { get; set; }   

        public ShippingMethod ShippingMethod { get; set; }  

        public int ShippingMethodId { get; set; }

        public User User { get; set; }

        public int UserId { get; set; } 

    }
}
