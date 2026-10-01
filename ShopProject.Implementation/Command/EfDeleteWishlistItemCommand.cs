using ShopProject.Application;
using ShopProject.Application.Command;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Command;

public class EfDeleteWishlistItemCommand : IDeleteWishlistItemCommand
{
    private readonly ShopProjectContext _context;
    private readonly IApplicationActor _actor;

    public EfDeleteWishlistItemCommand(ShopProjectContext context, IApplicationActor actor)
    {
        _context = context;
        _actor = actor;
    }

    public int Id => 25;
    public string Name => "Delete wishlist item";

    public void Execute(int id)
    {
        var item = _context.WishlistItems.FirstOrDefault(w => w.Id == id && w.UserId == _actor.Id)
                   ?? throw new EntityNotFoundException(nameof(Domain.WishlistItem), id);

        _context.WishlistItems.Remove(item);
        _context.SaveChanges();
    }
}
