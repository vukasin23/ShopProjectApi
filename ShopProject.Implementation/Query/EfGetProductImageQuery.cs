using ShopProject.Application.Exceptions;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetProductImageQuery:IGetProductImageQuery
{
    private readonly ShopProjectContext _context;

    public EfGetProductImageQuery(ShopProjectContext context)
    {
        _context = context;
    }

    public int Id => 50;
    public string Name => "Get one product image";

    public ProductImageResponse Execute(int id)
    {
        var image = _context.ProductImages.FirstOrDefault(x => x.Id == id && !x.Product.IsDeleted);

        if (image == null)
        {
            throw new EntityNotFoundException(nameof(Domain.ProductImage), id);
        }

        return new ProductImageResponse
        {
            Id = image.Id,
            ProductId = image.ProductId,
            ImageUrl = image.ImageUrl,
            AltText = image.AltText,
            IsPrimary = image.IsPrimary
        };
    }
}
