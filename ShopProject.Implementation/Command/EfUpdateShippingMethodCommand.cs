using FluentValidation;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;

namespace ShopProject.Implementation.Command;

public class EfUpdateShippingMethodCommand : IUpdateShippingMethodCommand
{
    private readonly ShopProjectContext _context;
    private readonly UpdateShippingMethodValidator _validator;

    public EfUpdateShippingMethodCommand(ShopProjectContext context, UpdateShippingMethodValidator validator)
    {
        _context = context;
        _validator = validator;
    }

    public int Id => 58;
    public string Name => "Update shipping method";

    public void Execute(UpdateShippingMethodDto request)
    {
        var shippingMethod = _context.ShippingMethods.FirstOrDefault(s => s.Id == request.Id)
            ?? throw new EntityNotFoundException(nameof(Domain.ShippingMethod), request.Id);

        _validator.ValidateAndThrow(request);

        shippingMethod.Name = request.Name;
        shippingMethod.Price = request.Price;
        shippingMethod.Description = request.Description;

        _context.SaveChanges();
    }
}
