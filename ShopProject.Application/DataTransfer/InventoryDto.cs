using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Application.DataTransfer
{
    public class InventoryDto
    {
        public int Quantity { get; set; }
        public int StoreId { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
    }
}
