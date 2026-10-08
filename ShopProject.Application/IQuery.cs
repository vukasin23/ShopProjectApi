using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Application
{
    public interface IQuery<TSearch, TData> : IUseCase where TSearch : PagedSearch
    {
        PagedResponse<TData> Execute(TSearch search);
        
    }
}
