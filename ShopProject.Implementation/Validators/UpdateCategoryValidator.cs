using FluentValidation;
using ShopProject.Application.DataTransfer;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Validators;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryValidator(ShopProjectContext context)
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(3)
            .WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(20)
            .WithMessage("Name must be at most 20 characters long.")
            // The category being updated may keep its own name.
            .Must((dto, name) => !context.Categories.Any(c => c.Name == name && c.Id != dto.Id))
            .WithMessage("Category with this name already exists.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(500)
            .WithMessage("Description must have maximum 500 characters.");

        RuleFor(x => x.ParentId)
            .Must(parentId => context.Categories.Any(c => c.Id == parentId))
            .WithMessage("Parent category does not exist.")
            .Must((dto, parentId) => !IsDescendant(context, dto.Id, parentId!.Value))
            .WithMessage("Category cannot be its own parent or a parent of its own parent.")
            .When(x => x.ParentId.HasValue);
    }

    // Walks up from the candidate parent; reaching the category being updated means it would create a cycle.
    private static bool IsDescendant(ShopProjectContext context, int categoryId, int candidateParentId)
    {
        int? currentId = candidateParentId;
        while (currentId.HasValue)
        {
            if (currentId == categoryId)
            {
                return true;
            }

            currentId = context.Categories.Where(c => c.Id == currentId).Select(c => c.ParentId).FirstOrDefault();
        }

        return false;
    }
}
