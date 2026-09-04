using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Application.DataTransfer
{
    public class ProductSpecificationDto
    {
        public int ProductId { get; set; }
        public string SpecificationName { get; set; }
        public string SpecificationValue { get; set; }
    }
}
