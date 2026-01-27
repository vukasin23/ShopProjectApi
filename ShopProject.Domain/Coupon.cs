using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Domain
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime ExpiryDate { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
