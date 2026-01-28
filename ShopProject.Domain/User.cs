using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Domain
{
    public class User
    {
       public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; } 
        public virtual IEnumerable<UseCase> UseCases { get; set; }

        public ICollection<WishlistItem> WishlistItems { get; set; }  
        public Cart Cart { get; set; }
        public int CartId { get; set; } 
        public ICollection<Order> Orders { get; set; }  


        public ICollection<Address> Addresses { get; set; } 


    }
}
