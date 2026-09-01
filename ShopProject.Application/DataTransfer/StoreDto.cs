using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ShopProject.Application.DataTransfer
{
    public class StoreDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
    }
}
