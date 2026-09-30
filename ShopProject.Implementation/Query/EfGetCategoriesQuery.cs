using ShopProject.Application;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.Application.Searches;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetCategoriesQuery:IGetCategoriesQuery
{
    private readonly ShopProjectContext _context;
    
    public EfGetCategoriesQuery(ShopProjectContext context)
    {
        _context = context;
    }
    public PagedResponse<CategoryResponse> Execute(CategorySearch search)
    {
        var query = _context.Categories.AsQueryable();
        if (!string.IsNullOrEmpty(search.Name))
        {
            query = query.Where(x => x.Name.Contains(search.Name));
        }

        if (search.ParentId.HasValue)
        {
            query = query.Where(x => x.ParentId == search.ParentId);
        }

        var totalCount = query.Count();

        var data = query
            .OrderBy(x => x.Id)
            .Skip((search.PageNumber - 1) * search.PerPage)
            .Take(search.PerPage)
            .Select(x => new CategoryResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ParentId = x.ParentId,
                ParentName = x.Parent != null ? x.Parent.Name : null
            })
            .ToList();

        return new PagedResponse<CategoryResponse> 
        {
            CurrentPage = search.PageNumber,
            ItemsPerPage = search.PerPage,
            TotalCount = totalCount,
            PagesCount = (int)Math.Ceiling(totalCount / (double)search.PerPage),
            Data = data
        };
    }

    public int Id => 19;
    public string Name => "Get all categories";
}
