using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }  

        public int CategoryId { get; set; } 

        public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

        public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

        public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

        public ICollection<ProductSpecification> ProductSpecification { get; set; } = new List<ProductSpecification>();

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    }
}
