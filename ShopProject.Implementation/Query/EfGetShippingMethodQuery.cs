using ShopProject.Application;
using ShopProject.Application.Exceptions;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetShippingMethodQuery:IGetShippingMethodQuery
{
    private readonly ShopProjectContext _context;

    public EfGetShippingMethodQuery(ShopProjectContext context)
    {
        _context = context;
    }

    public ShippingMethodResponse Execute(int id)
    {
        var shippingMethod = _context.ShippingMethods.Find(id);

        if (shippingMethod == null)
        {
            throw new EntityNotFoundException(nameof(Domain.ShippingMethod), id);
        }

        return new ShippingMethodResponse
        {
            Id = shippingMethod.Id,
            Name = shippingMethod.Name,
            Price = shippingMethod.Price,
            Description = shippingMethod.Description
        };
    }

    public int Id => 38;
    public string Name => "Get one shipping method";
}
