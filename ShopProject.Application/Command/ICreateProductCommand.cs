using ShopProject.Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Application.Command
{
    public interface ICreateProductCommand:ICommand<ProductDto>
    {
    }
}
