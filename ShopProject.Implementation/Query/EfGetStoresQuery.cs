using ShopProject.Application;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.Application.Searches;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetStoresQuery:IGetStoresQuery
{
    private readonly ShopProjectContext _context;
    
    public EfGetStoresQuery(ShopProjectContext context)
    {
        _context = context;
    }
    public PagedResponse<StoreResponse> Execute(StoreSearch search)
    {
        var query = _context.Stores.AsQueryable();
        if (!string.IsNullOrEmpty(search.Name))
        {
            query = query.Where(x => x.Name.Contains(search.Name));
        }

        if (!string.IsNullOrEmpty(search.City))
        {
            query = query.Where(x => x.City.Contains(search.City));
        }

        if (search.IsActive.HasValue)
        {
            query = query.Where(x => x.IsActive == search.IsActive);
        }

        var totalCount = query.Count();

        var data = query
            .OrderBy(x => x.Id)
            .Skip((search.PageNumber - 1) * search.PerPage)
            .Take(search.PerPage)
            .Select(x => new StoreResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                City = x.City,
                Address = x.Address,
                Phone = x.Phone,
                IsActive = x.IsActive
            })
            .ToList();

        return new PagedResponse<StoreResponse> 
        {
            CurrentPage = search.PageNumber,
            ItemsPerPage = search.PerPage,
            TotalCount = totalCount,
            PagesCount = (int)Math.Ceiling(totalCount / (double)search.PerPage),
            Data = data
        };
    }

    public int Id => 20;
    public string Name => "Get all stores";
}
