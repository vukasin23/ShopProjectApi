using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Application.DataTransfer
{
    public class CartDto
    {
        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
