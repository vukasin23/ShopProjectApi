using ShopProject.Application.Command;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Command;

public class EfDeleteStoreCommand : IDeleteStoreCommand
{
    private readonly ShopProjectContext _context;

    public EfDeleteStoreCommand(ShopProjectContext context)
    {
        _context = context;
    }

    public int Id => 27;
    public string Name => "Delete store";

    public void Execute(int id)
    {
        var store = _context.Stores.FirstOrDefault(s => s.Id == id)
                    ?? throw new EntityNotFoundException(nameof(Domain.Store), id);

        // Inventory -> Store is a cascade in the database, so a store that still holds stock must not be removed.
        // Empty (zero quantity) inventory rows are removed together with the store.
        if (_context.Inventories.Any(i => i.StoreId == id && i.Quantity > 0))
        {
            throw new ConflictException("Store cannot be deleted because it still has products in stock.");
        }

        _context.Stores.Remove(store);
        _context.SaveChanges();
    }
}
