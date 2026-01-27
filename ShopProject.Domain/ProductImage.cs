using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Domain
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string ImageUrl { get; set; }
        public string AltText { get; set; }
        public bool IsPrimary { get; set; }
    }
}
