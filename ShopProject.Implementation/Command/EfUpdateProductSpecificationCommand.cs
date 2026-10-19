using FluentValidation;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;

namespace ShopProject.Implementation.Command;

public class EfUpdateProductSpecificationCommand : IUpdateProductSpecificationCommand
{
    private readonly ShopProjectContext _context;
    private readonly UpdateProductSpecificationValidator _validator;

    public EfUpdateProductSpecificationCommand(ShopProjectContext context, UpdateProductSpecificationValidator validator)
    {
        _context = context;
        _validator = validator;
    }

    public int Id => 63;
    public string Name => "Update product specification";

    public void Execute(UpdateProductSpecificationDto request)
    {
        var productSpecification = _context.ProductSpecifications.FirstOrDefault(p => p.Id == request.Id)
            ?? throw new EntityNotFoundException(nameof(Domain.ProductSpecification), request.Id);

        _validator.ValidateAndThrow(request);

        productSpecification.SpecificationName = request.SpecificationName;
        productSpecification.SpecificationValue = request.SpecificationValue;

        _context.SaveChanges();
    }
}
