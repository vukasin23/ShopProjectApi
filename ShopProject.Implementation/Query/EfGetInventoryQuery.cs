using Microsoft.EntityFrameworkCore;
using ShopProject.Application;
using ShopProject.Application.Exceptions;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetInventoryQuery:IGetInventoryQuery
{
    private readonly ShopProjectContext _context;

    public EfGetInventoryQuery(ShopProjectContext context)
    {
        _context = context;
    }

    public InventoryResponse Execute(int id)
    {
        var inventory = _context.Inventories
            .Include(x => x.Store)
            .Include(x => x.Product)
            .FirstOrDefault(x => x.Id == id && !x.Product.IsDeleted);

        if (inventory == null)
        {
            throw new EntityNotFoundException(nameof(Domain.Inventory), id);
        }

        return new InventoryResponse
        {
            Id = inventory.Id,
            ProductId = inventory.ProductId,
            ProductName = inventory.Product.Name,
            StoreId = inventory.StoreId,
            StoreName = inventory.Store.Name,
            Quantity = inventory.Quantity,
            LastUpdated = inventory.LastUpdated
        };
    }

    public int Id => 40;
    public string Name => "Get one inventory";
}
