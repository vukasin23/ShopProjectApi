using ShopProject.Application.Command;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Command;

public class EfDeleteInventoryCommand : IDeleteInventoryCommand
{
    private readonly ShopProjectContext _context;

    public EfDeleteInventoryCommand(ShopProjectContext context)
    {
        _context = context;
    }

    public int Id => 32;
    public string Name => "Delete inventory";

    public void Execute(int id)
    {
        var inventory = _context.Inventories.FirstOrDefault(i => i.Id == id)
                        ?? throw new EntityNotFoundException(nameof(Domain.Inventory), id);

        _context.Inventories.Remove(inventory);
        _context.SaveChanges();
    }
}
