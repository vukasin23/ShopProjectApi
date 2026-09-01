using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Application.DataTransfer
{
    public class AddressDto
    {
        public int UserId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }    
        public string ZipCode { get; set; }    
        public string Country { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

    }
}
