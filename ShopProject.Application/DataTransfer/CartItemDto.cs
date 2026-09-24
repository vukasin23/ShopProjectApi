using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Application.DataTransfer
{
    public class CartItemDto
    {
        public int CartId { get; set; } 
    
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public DateTime AddedAt { get; set; } = DateTime.Now;
    }
}
