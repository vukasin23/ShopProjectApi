using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Application
{
    public abstract class PagedSearch
    {
        public int PageNumber { get; set; } = 1;
        public int PerPage { get; set; } = 10;

    }
}
