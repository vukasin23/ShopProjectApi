using FluentValidation;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;

namespace ShopProject.Implementation.Command;

public class EfUpdateOrderCommand : IUpdateOrderCommand
{
    private readonly ShopProjectContext _context;
    private readonly UpdateOrderStatusValidator _validator;

    public EfUpdateOrderCommand(ShopProjectContext context, UpdateOrderStatusValidator validator)
    {
        _context = context;
        _validator = validator;
    }

    public int Id => 66;
    public string Name => "Update order status";

    public void Execute(UpdateOrderStatusDto request)
    {
        var order = _context.Orders.FirstOrDefault(o => o.Id == request.Id)
            ?? throw new EntityNotFoundException(nameof(Domain.Order), request.Id);

        _validator.ValidateAndThrow(request);

        order.StatusId = request.StatusId;

        _context.SaveChanges();
    }
}
