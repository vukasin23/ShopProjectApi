using ShopProject.Application.DataTransfer;

namespace ShopProject.Application.Command
{
    public interface ICreateOrderCommand:ICommand<OrderDto>
    {
        void Execute(OrderDto request);
    }
}
