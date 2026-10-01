using ShopProject.Application.Command;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Command;

public class EfDeleteProductImageCommand : IDeleteProductImageCommand
{
    private readonly ShopProjectContext _context;

    public EfDeleteProductImageCommand(ShopProjectContext context)
    {
        _context = context;
    }

    public int Id => 30;
    public string Name => "Delete product image";

    public void Execute(int id)
    {
        var image = _context.ProductImages.FirstOrDefault(i => i.Id == id)
                    ?? throw new EntityNotFoundException(nameof(Domain.ProductImage), id);

        _context.ProductImages.Remove(image);
        _context.SaveChanges();
    }
}
