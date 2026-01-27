using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Domain
{
    public class ProductSpecification
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
