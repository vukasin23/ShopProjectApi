using FluentValidation;
using ShopProject.Application;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;

namespace ShopProject.Implementation.Command;

public class EfUpdateAddressCommand : IUpdateAddressCommand
{
    private readonly ShopProjectContext _context;
    private readonly UpdateAddressValidator _validator;
    private readonly IApplicationActor _actor;

    public EfUpdateAddressCommand(ShopProjectContext context, UpdateAddressValidator validator, IApplicationActor actor)
    {
        _context = context;
        _validator = validator;
        _actor = actor;
    }

    public int Id => 64;
    public string Name => "Update address";

    public void Execute(UpdateAddressDto request)
    {
        // A user can change only their own address.
        var address = _context.Addresses.FirstOrDefault(a => a.Id == request.Id && a.UserId == _actor.Id)
            ?? throw new EntityNotFoundException(nameof(Domain.Address), request.Id);

        _validator.ValidateAndThrow(request);

        address.Street = request.Street;
        address.City = request.City;
        address.State = request.State;
        address.ZipCode = request.ZipCode;
        address.Country = request.Country;
        address.UpdatedAt = DateTime.UtcNow;

        _context.SaveChanges();
    }
}
