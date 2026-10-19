using FluentValidation;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;

namespace ShopProject.Implementation.Command;

public class EfUpdateStoreCommand : IUpdateStoreCommand
{
    private readonly ShopProjectContext _context;
    private readonly UpdateStoreValidator _validator;

    public EfUpdateStoreCommand(ShopProjectContext context, UpdateStoreValidator validator)
    {
        _context = context;
        _validator = validator;
    }

    public int Id => 57;
    public string Name => "Update store";

    public void Execute(UpdateStoreDto request)
    {
        var store = _context.Stores.FirstOrDefault(s => s.Id == request.Id)
            ?? throw new EntityNotFoundException(nameof(Domain.Store), request.Id);

        _validator.ValidateAndThrow(request);

        store.Name = request.Name;
        store.Description = request.Description;
        store.City = request.City;
        store.Address = request.Address;
        store.Phone = request.Phone;
        store.IsActive = request.IsActive!.Value;

        _context.SaveChanges();
    }
}
