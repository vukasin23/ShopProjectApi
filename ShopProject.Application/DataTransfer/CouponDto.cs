using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Application.DataTransfer
{
    public class CouponDto
    {
        public string Code { get; set; }
        public int DiscountAmount { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
