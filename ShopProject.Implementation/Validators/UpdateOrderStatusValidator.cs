using FluentValidation;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;
using ShopProject.Domain;

namespace ShopProject.Implementation.Validators;

public class UpdateOrderStatusValidator : AbstractValidator<UpdateOrderStatusDto>
{
    public UpdateOrderStatusValidator(ShopProjectContext context)
    {
        RuleFor(x => x.StatusId)
            .Must(statusId => context.Statuses.Any(s => s.Id == statusId))
            .WithMessage("Status does not exist.");

        // Delivered and cancelled are final, such an order can no longer change.
        RuleFor(x => x.Id)
            .Must(id => !context.Orders.Any(o => o.Id == id && (o.StatusId == Status.Delivered || o.StatusId == Status.Cancelled)))
            .WithMessage("Status of a delivered or cancelled order cannot be changed.");
    }
}
