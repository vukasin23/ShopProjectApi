using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Application.DataTransfer
{
    public class ProductImageDto
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public bool IsPrimary { get; set; }
    }
}
