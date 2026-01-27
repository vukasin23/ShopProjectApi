using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Domain
{
    public class OrderLine
    {
        public int Id { get; set; }

        public Order Order { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal => Quantity * UnitPrice;
    }
}
