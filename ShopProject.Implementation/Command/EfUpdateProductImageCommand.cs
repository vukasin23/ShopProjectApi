using FluentValidation;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;

namespace ShopProject.Implementation.Command;

public class EfUpdateProductImageCommand : IUpdateProductImageCommand
{
    private readonly ShopProjectContext _context;
    private readonly UpdateProductImageValidator _validator;

    public EfUpdateProductImageCommand(ShopProjectContext context, UpdateProductImageValidator validator)
    {
        _context = context;
        _validator = validator;
    }

    public int Id => 62;
    public string Name => "Update product image";

    public void Execute(UpdateProductImageDto request)
    {
        var productImage = _context.ProductImages.FirstOrDefault(p => p.Id == request.Id)
            ?? throw new EntityNotFoundException(nameof(Domain.ProductImage), request.Id);

        _validator.ValidateAndThrow(request);

        productImage.ImageUrl = request.ImageUrl;
        productImage.AltText = request.AltText;
        productImage.IsPrimary = request.IsPrimary!.Value;

        _context.SaveChanges();
    }
}
