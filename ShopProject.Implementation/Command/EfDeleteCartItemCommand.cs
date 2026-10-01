using ShopProject.Application;
using ShopProject.Application.Command;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Command;

public class EfDeleteCartItemCommand : IDeleteCartItemCommand
{
    private readonly ShopProjectContext _context;
    private readonly IApplicationActor _actor;

    public EfDeleteCartItemCommand(ShopProjectContext context, IApplicationActor actor)
    {
        _context = context;
        _actor = actor;
    }

    public int Id => 24;
    public string Name => "Delete cart item";

    public void Execute(int id)
    {
        // Items from someone else's cart are reported as not found, so their existence is not revealed.
        var cartItem = _context.CartItems.FirstOrDefault(ci => ci.Id == id && ci.Cart.UserId == _actor.Id)
                       ?? throw new EntityNotFoundException(nameof(Domain.CartItem), id);

        _context.CartItems.Remove(cartItem);
        _context.SaveChanges();
    }
}
