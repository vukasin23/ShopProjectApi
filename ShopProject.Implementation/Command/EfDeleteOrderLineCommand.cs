using ShopProject.Application.Exceptions;
using ShopProject.Application.Query;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Command;

public class EfDeleteOrderLineCommand:IDeleteOrderLineCommand
{
    private readonly ShopProjectContext _context;

    public EfDeleteOrderLineCommand(ShopProjectContext context)
    {
        _context = context;
    }
    public int Id => 50;
    public string Name => "Delete Order Line";

    public void Execute(int request)
    {
        var orderLine = _context.OrderLines.FirstOrDefault(x => x.Id == request) ??throw new EntityNotFoundException("Order Line not found");;
        
        _context.OrderLines.Remove(orderLine);
        _context.SaveChanges();
    }
}