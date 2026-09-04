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
        public ICollection<UseCase> UseCases { get; set; } = new List<UseCase>();

        public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
        public Cart Cart { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();


        public ICollection<Address> Addresses { get; set; } = new List<Address>();

        public ICollection<UseCaseLog> usecaseLogs { get; set; } = new List<UseCaseLog>();

    }
}
