using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Domain
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public string City { get; set; }
        public string Address { get; set; } 
        public string Phone { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Inventory> Inventories { get; set; } 
    }
}
