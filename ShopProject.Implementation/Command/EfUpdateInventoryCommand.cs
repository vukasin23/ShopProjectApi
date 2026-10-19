using FluentValidation;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;

namespace ShopProject.Implementation.Command;

public class EfUpdateInventoryCommand : IUpdateInventoryCommand
{
    private readonly ShopProjectContext _context;
    private readonly UpdateInventoryValidator _validator;

    public EfUpdateInventoryCommand(ShopProjectContext context, UpdateInventoryValidator validator)
    {
        _context = context;
        _validator = validator;
    }

    public int Id => 60;
    public string Name => "Update inventory";

    public void Execute(UpdateInventoryDto request)
    {
        var inventory = _context.Inventories.FirstOrDefault(i => i.Id == request.Id)
            ?? throw new EntityNotFoundException(nameof(Domain.Inventory), request.Id);

        _validator.ValidateAndThrow(request);

        inventory.Quantity = request.Quantity;
        inventory.LastUpdated = DateTime.Now;

        _context.SaveChanges();
    }
}
