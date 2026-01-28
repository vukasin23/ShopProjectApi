using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Application
{
    public interface IErrorLogger
    {
        void Log(Exception ex);
    }
}
