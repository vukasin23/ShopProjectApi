using ShopProject.Application.Exceptions;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetProductSpecificationQuery:IGetProductSpecificationQuery
{
    private readonly ShopProjectContext _context;

    public EfGetProductSpecificationQuery(ShopProjectContext context)
    {
        _context = context;
    }

    public int Id => 52;
    public string Name => "Get one product specification";

    public ProductSpecificationResponse Execute(int id)
    {
        var specification = _context.ProductSpecifications.FirstOrDefault(x => x.Id == id && !x.Product.IsDeleted);

        if (specification == null)
        {
            throw new EntityNotFoundException(nameof(Domain.ProductSpecification), id);
        }

        return new ProductSpecificationResponse
        {
            Id = specification.Id,
            ProductId = specification.ProductId,
            SpecificationName = specification.SpecificationName,
            SpecificationValue = specification.SpecificationValue
        };
    }
}
