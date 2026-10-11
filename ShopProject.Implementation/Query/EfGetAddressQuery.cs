using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.Application.Searches;
using ShopProject.DataAccess;
using ShopProject.Application.Exceptions;

namespace ShopProject.Implementation.Query;

public class EfGetAddressQuery:IGetAddresQuery
{
    private readonly ShopProjectContext _context;
    public EfGetAddressQuery(ShopProjectContext context)
    {
        _context = context;
    }
    public AddressResponse Execute(int id)
    {
        var address = _context.Addresses.FirstOrDefault(a=>a.Id == id && !a.IsDeleted);
        
        if (address == null)
        {
            throw new EntityNotFoundException(nameof(Domain.Inventory), id);
        }

        return new AddressResponse
        {
            Id =  address.Id,
            UserId = address.UserId,
            Street = address.Street,
            City = address.City,
            State = address.State,
            ZipCode = address.ZipCode,
            Country = address.Country
            
        };
    }

    public int Id => 44;
    public string Name => "Get address of user";
}