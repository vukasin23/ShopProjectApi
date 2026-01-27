using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Domain
{
    public class ShippingMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
