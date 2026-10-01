using ShopProject.Application;
using ShopProject.Application.Command;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Command;

public class EfDeleteAddressCommand : IDeleteAddressCommand
{
    private readonly ShopProjectContext _context;
    private readonly IApplicationActor _actor;

    public EfDeleteAddressCommand(ShopProjectContext context, IApplicationActor actor)
    {
        _context = context;
        _actor = actor;
    }

    public int Id => 34;
    public string Name => "Delete address";

    public void Execute(int id)
    {
        // Soft delete: past orders still reference the address they were shipped to.
        var address = _context.Addresses.FirstOrDefault(a => a.Id == id && a.UserId == _actor.Id)
                      ?? throw new EntityNotFoundException(nameof(Domain.Address), id);

        address.IsDeleted = true;
        address.UpdatedAt = DateTime.UtcNow;
        _context.SaveChanges();
    }
}
