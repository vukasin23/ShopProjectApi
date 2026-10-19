using ShopProject.Application.Command;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Command;

public class EfDeleteUserCommand : IDeleteUserCommand
{
    private readonly ShopProjectContext _context;

    public EfDeleteUserCommand(ShopProjectContext context)
    {
        _context = context;
    }

    public int Id => 54;
    public string Name => "Delete user";

    public void Execute(int id)
    {
        // Soft delete: orders keep pointing at the user, so the row must stay.
        // There is no global query filter on users, because it would hide those orders from the order list.
        var user = _context.Users.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
                   ?? throw new EntityNotFoundException(nameof(Domain.User), id);

        user.IsDeleted = true;

        // A deleted user has no use for a cart, wishlist or saved addresses.
        _context.CartItems.RemoveRange(_context.CartItems.Where(ci => ci.Cart.UserId == id));
        _context.Carts.RemoveRange(_context.Carts.Where(c => c.UserId == id));
        _context.WishlistItems.RemoveRange(_context.WishlistItems.Where(w => w.UserId == id));

        foreach (var address in _context.Addresses.Where(a => a.UserId == id))
        {
            address.IsDeleted = true;
            address.UpdatedAt = DateTime.UtcNow;
        }

        _context.SaveChanges();
    }
}
