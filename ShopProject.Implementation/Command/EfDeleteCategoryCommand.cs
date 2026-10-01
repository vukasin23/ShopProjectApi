using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Command;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Command;

public class EfDeleteCategoryCommand : IDeleteCategoryCommand
{
    private readonly ShopProjectContext _context;

    public EfDeleteCategoryCommand(ShopProjectContext context)
    {
        _context = context;
    }

    public int Id => 26;
    public string Name => "Delete category";

    public void Execute(int id)
    {
        var category = _context.Categories.FirstOrDefault(c => c.Id == id)
                       ?? throw new EntityNotFoundException(nameof(Domain.Category), id);

        // Product -> Category is a cascade in the database, so this must be checked here or products would be wiped.
        // IgnoreQueryFilters also counts soft-deleted products, which still reference the category.
        if (_context.Products.IgnoreQueryFilters().Any(p => p.CategoryId == id))
        {
            throw new ConflictException("Category cannot be deleted because it contains products.");
        }

        if (_context.Categories.Any(c => c.ParentId == id))
        {
            throw new ConflictException("Category cannot be deleted because it has subcategories.");
        }

        _context.Categories.Remove(category);
        _context.SaveChanges();
    }
}
