using FluentValidation;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;

namespace ShopProject.Implementation.Command;

public class EfUpdateCategoryCommand : IUpdateCategoryCommand
{
    private readonly ShopProjectContext _context;
    private readonly UpdateCategoryValidator _validator;

    public EfUpdateCategoryCommand(ShopProjectContext context, UpdateCategoryValidator validator)
    {
        _context = context;
        _validator = validator;
    }

    public int Id => 37;
    public string Name => "Update category using EF";

    public void Execute(UpdateCategoryDto request)
    {
        var category = _context.Categories.FirstOrDefault(c => c.Id == request.Id)
                       ?? throw new EntityNotFoundException(nameof(Domain.Category), request.Id);

        _validator.ValidateAndThrow(request);

        category.Name = request.Name;
        category.Description = request.Description;
        category.ParentId = request.ParentId;

        _context.SaveChanges();
    }
}
