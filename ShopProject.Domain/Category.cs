using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public ICollection<Category> Children { get; set; }
        public int? ParentId { get; set; }    
        public Category? Parent { get; set; }
    }
}
