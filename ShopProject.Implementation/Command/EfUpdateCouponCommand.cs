using FluentValidation;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;

namespace ShopProject.Implementation.Command;

public class EfUpdateCouponCommand : IUpdateCouponCommand
{
    private readonly ShopProjectContext _context;
    private readonly UpdateCouponValidator _validator;

    public EfUpdateCouponCommand(ShopProjectContext context, UpdateCouponValidator validator)
    {
        _context = context;
        _validator = validator;
    }

    public int Id => 59;
    public string Name => "Update coupon";

    public void Execute(UpdateCouponDto request)
    {
        var coupon = _context.Coupons.FirstOrDefault(c => c.Id == request.Id)
            ?? throw new EntityNotFoundException(nameof(Domain.Coupon), request.Id);

        _validator.ValidateAndThrow(request);

        coupon.Code = request.Code;
        coupon.DiscountAmount = request.DiscountAmount;
        coupon.ExpiryDate = request.ExpiryDate;

        _context.SaveChanges();
    }
}
