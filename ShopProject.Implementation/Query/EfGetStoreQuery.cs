using ShopProject.Application;
using ShopProject.Application.Exceptions;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetStoreQuery:IGetStoreQuery
{
    private readonly ShopProjectContext _context;

    public EfGetStoreQuery(ShopProjectContext context)
    {
        _context = context;
    }

    public StoreResponse Execute(int id)
    {
        var store = _context.Stores.Find(id);

        if (store == null)
        {
            throw new EntityNotFoundException(nameof(Domain.Store), id);
        }

        return new StoreResponse
        {
            Id = store.Id,
            Name = store.Name,
            Description = store.Description,
            City = store.City,
            Address = store.Address,
            Phone = store.Phone,
            IsActive = store.IsActive
        };
    }

    public int Id => 37;
    public string Name => "Get one store";
}
