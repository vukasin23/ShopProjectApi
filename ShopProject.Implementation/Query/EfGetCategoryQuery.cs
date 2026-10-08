using ShopProject.Application.Exceptions;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetCategoryQuery : IGetCategoryQuery
{
    private readonly ShopProjectContext _context;

    public EfGetCategoryQuery(ShopProjectContext context)
    {
        _context = context;
    }

    public CategoryResponse Execute(int id)
    {
        return _context.Categories
                   .Where(x => x.Id == id)
                   .Select(x => new CategoryResponse
                   {
                       Id = x.Id,
                       Name = x.Name,
                       Description = x.Description,
                       ParentId = x.ParentId,
                       ParentName = x.Parent != null ? x.Parent.Name : null
                   })
                   .FirstOrDefault()
               ?? throw new EntityNotFoundException(nameof(Domain.Category), id);
    }

    public int Id => 36;
    public string Name => "Get category by id";
}
