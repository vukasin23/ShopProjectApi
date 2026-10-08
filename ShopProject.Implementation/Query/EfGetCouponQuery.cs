using ShopProject.Application;
using ShopProject.Application.Exceptions;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetCouponQuery:IGetCouponQuery
{
    private readonly ShopProjectContext _context;

    public EfGetCouponQuery(ShopProjectContext context)
    {
        _context = context;
    }

    public CouponResponse Execute(int id)
    {
        var coupon = _context.Coupons.Find(id);

        if (coupon == null)
        {
            throw new EntityNotFoundException(nameof(Domain.Coupon), id);
        }

        return new CouponResponse
        {
            Id = coupon.Id,
            Code = coupon.Code,
            DiscountAmount = coupon.DiscountAmount,
            ExpiryDate = coupon.ExpiryDate
        };
    }

    public int Id => 39;
    public string Name => "Get one coupon";
}
