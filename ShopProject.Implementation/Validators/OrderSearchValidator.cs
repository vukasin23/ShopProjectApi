using FluentValidation;
using ShopProject.Application.Searches;

namespace ShopProject.Implementation.Validators
{
    public class OrderSearchValidator : AbstractValidator<OrderSearch>
    {
        public OrderSearchValidator()
        {
            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Minimum price can't be negative.")
                .When(x => x.MinPrice.HasValue);

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Maximum price can't be negative.")
                .When(x => x.MaxPrice.HasValue);

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(x => x.MinPrice!.Value).WithMessage("Maximum price can't be lower than minimum price.")
                .When(x => x.MinPrice.HasValue && x.MaxPrice.HasValue);
        }
    }
}
