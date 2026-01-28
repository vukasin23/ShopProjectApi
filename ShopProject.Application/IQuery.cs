using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Application
{
    public interface IQuery<TSearch, TResult>: IUseCase
    {
        TResult Execute(TSearch search);
    }
}
