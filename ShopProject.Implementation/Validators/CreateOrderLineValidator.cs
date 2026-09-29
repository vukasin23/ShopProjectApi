using FluentValidation;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Validators;

public class CreateOrderLineValidator : AbstractValidator<OrderLineDto>
{
    public CreateOrderLineValidator(ShopProjectContext _context)
    {
        RuleFor(o => o.OrderId)
            .GreaterThan(0)
            .WithMessage("OrderId must be greater than 0.")
            .Must(orderId => _context.Orders.Any(o => o.Id == orderId))
            .WithMessage("Order does not exist.");

        RuleFor(o => o.ProductId)
            .GreaterThan(0)
            .WithMessage("ProductId must be greater than 0.")
            .Must(productId => _context.Products.Any(p => p.Id == productId))
            .WithMessage("Product does not exist.");

        RuleFor(o => o.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0.");

        RuleFor(o => o.UnitPrice)
            .GreaterThan(0)
            .WithMessage("UnitPrice must be greater than 0.");

        RuleFor(o => o)
            .Must(dto => !_context.OrderLines.Any(ol =>
                ol.OrderId == dto.OrderId &&
                ol.ProductId == dto.ProductId))
            .WithMessage("This product is already in the order.")
            .When(o => o.OrderId > 0 && o.ProductId > 0);
    }
}
