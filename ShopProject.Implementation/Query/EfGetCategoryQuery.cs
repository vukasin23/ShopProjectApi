using ShopProject.Application;
using ShopProject.Application.Exceptions;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetCategoryQuery:IGetCategoryQuery
{
    private readonly ShopProjectContext _context;

    public EfGetCategoryQuery(ShopProjectContext context)
    {
        _context = context;
    }

    public CategoryResponse Execute(int id)
    {
        var category = _context.Categories.Find(id);

        if (category == null)
        {
            throw new EntityNotFoundException(nameof(Domain.Category), id);
        }

        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ParentId = category.ParentId
        };
    }

    public int Id => 36;
    public string Name => "Get one category";
}