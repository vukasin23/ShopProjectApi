using ShopProject.Application.Command;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Command;

public class EfDeleteShippingMethodCommand : IDeleteShippingMethodCommand
{
    private readonly ShopProjectContext _context;

    public EfDeleteShippingMethodCommand(ShopProjectContext context)
    {
        _context = context;
    }

    public int Id => 28;
    public string Name => "Delete shipping method";

    public void Execute(int id)
    {
        var shippingMethod = _context.ShippingMethods.FirstOrDefault(s => s.Id == id)
                             ?? throw new EntityNotFoundException(nameof(Domain.ShippingMethod), id);

        
        if (_context.Orders.Any(o => o.ShippingMethodId == id))
        {
            throw new ConflictException("Shipping method cannot be deleted because it is used by existing orders.");
        }

        _context.ShippingMethods.Remove(shippingMethod);
        _context.SaveChanges();
    }
}
