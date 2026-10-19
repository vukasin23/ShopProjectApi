using System.Collections.Generic;

namespace ShopProject.Domain
{
    public class Status
    {
        // Ids of the seeded statuses, so code does not depend on their names.
        public const int Pending = 1;
        public const int Processing = 2;
        public const int Shipped = 3;
        public const int Delivered = 4;
        public const int Cancelled = 5;

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
