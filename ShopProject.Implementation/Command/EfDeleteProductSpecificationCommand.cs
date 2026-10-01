using ShopProject.Application.Command;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Command;

public class EfDeleteProductSpecificationCommand : IDeleteProductSpecificationCommand
{
    private readonly ShopProjectContext _context;

    public EfDeleteProductSpecificationCommand(ShopProjectContext context)
    {
        _context = context;
    }

    public int Id => 31;
    public string Name => "Delete product specification";

    public void Execute(int id)
    {
        var specification = _context.ProductSpecifications.FirstOrDefault(s => s.Id == id)
                            ?? throw new EntityNotFoundException(nameof(Domain.ProductSpecification), id);

        _context.ProductSpecifications.Remove(specification);
        _context.SaveChanges();
    }
}
