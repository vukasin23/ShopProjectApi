using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Application.DataTransfer
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; } 
    }
}
