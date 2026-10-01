using ShopProject.Application.Command;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Command;

public class EfDeleteProductCommand : IDeleteProductCommand
{
    private readonly ShopProjectContext _context;

    public EfDeleteProductCommand(ShopProjectContext context)
    {
        _context = context;
    }

    public int Id => 33;
    public string Name => "Delete product";

    public void Execute(int id)
    {
        // Soft delete: existing orders keep pointing at the product, so the row must stay.
        // A product that is already deleted is hidden by the query filter and reported as not found.
        var product = _context.Products.FirstOrDefault(p => p.Id == id)
                      ?? throw new EntityNotFoundException(nameof(Domain.Product), id);

        product.IsDeleted = true;

        // A deleted product must not linger in carts or wishlists.
        _context.CartItems.RemoveRange(_context.CartItems.Where(ci => ci.ProductId == id));
        _context.WishlistItems.RemoveRange(_context.WishlistItems.Where(w => w.ProductId == id));

        _context.SaveChanges();
    }
}
