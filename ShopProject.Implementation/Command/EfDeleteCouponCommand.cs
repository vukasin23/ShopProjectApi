using ShopProject.Application.Command;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Command;

public class EfDeleteCouponCommand : IDeleteCouponCommand
{
    private readonly ShopProjectContext _context;

    public EfDeleteCouponCommand(ShopProjectContext context)
    {
        _context = context;
    }

    public int Id => 29;
    public string Name => "Delete coupon";

    public void Execute(int id)
    {
        var coupon = _context.Coupons.FirstOrDefault(c => c.Id == id)
                     ?? throw new EntityNotFoundException(nameof(Domain.Coupon), id);

        if (_context.Orders.Any(o => o.CouponId == id))
        {
            throw new ConflictException("Coupon cannot be deleted because it is used by existing orders.");
        }

        _context.Coupons.Remove(coupon);
        _context.SaveChanges();
    }
}
