using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Domain
{
    public class Inventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public Store Store { get; set; }
        public int StoreId { get; set; }    
        public DateTime LastUpdated { get; set; }
    }
}
