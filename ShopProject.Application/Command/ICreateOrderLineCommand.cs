using System;
using System.Collections.Generic;
using System.Text;
using ShopProject.Application.DataTransfer;

namespace ShopProject.Application.Command
{
    public interface ICreateOrderLineCommand:ICommand<OrderLineDto>
    {
   
    }
}
