using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Application
{
    public class PagedResponse<T>
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }   
        public int TotalCount { get; set; }
        public int PagesCount { get; set; }

        public T Data { get; set; }
    }
}
