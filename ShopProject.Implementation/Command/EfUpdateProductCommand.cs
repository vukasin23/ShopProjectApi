using FluentValidation;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;

namespace ShopProject.Implementation.Command;

public class EfUpdateProductCommand : IUpdateProductCommand
{
    private readonly ShopProjectContext _context;
    private readonly UpdateProductValidator _validator;

    public EfUpdateProductCommand(ShopProjectContext context, UpdateProductValidator validator)
    {
        _context = context;
        _validator = validator;
    }

    public int Id => 61;
    public string Name => "Update product";

    public void Execute(UpdateProductDto request)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == request.Id)
            ?? throw new EntityNotFoundException(nameof(Domain.Product), request.Id);

        _validator.ValidateAndThrow(request);

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.CategoryId = request.CategoryId;

        _context.SaveChanges();
    }
}
