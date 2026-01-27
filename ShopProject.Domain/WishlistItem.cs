using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Domain
{
    public class WishlistItem
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
