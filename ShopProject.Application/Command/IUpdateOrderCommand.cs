using ShopProject.Application.DataTransfer;

namespace ShopProject.Application.Command;

public interface IUpdateOrderCommand : ICommand<UpdateOrderStatusDto>
{
}
