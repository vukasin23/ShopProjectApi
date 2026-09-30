using ShopProject.Application;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.Application.Searches;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetShippingMethodsQuery:IGetShippingMethodsQuery
{
    private readonly ShopProjectContext _context;
    
    public EfGetShippingMethodsQuery(ShopProjectContext context)
    {
        _context = context;
    }
    public PagedResponse<ShippingMethodResponse> Execute(ShippingMethodSearch search)
    {
        var query = _context.ShippingMethods.AsQueryable();
        if (!string.IsNullOrEmpty(search.Name))
        {
            query = query.Where(x => x.Name.Contains(search.Name));
        }

        if (search.MinPrice.HasValue)
        {
            query = query.Where(x => x.Price >= search.MinPrice);
        }

        if (search.MaxPrice.HasValue)
        {
            query = query.Where(x => x.Price <= search.MaxPrice);
        }

        var totalCount = query.Count();

        var data = query
            .OrderBy(x => x.Id)
            .Skip((search.PageNumber - 1) * search.PerPage)
            .Take(search.PerPage)
            .Select(x => new ShippingMethodResponse
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Description = x.Description
            })
            .ToList();

        return new PagedResponse<ShippingMethodResponse> 
        {
            CurrentPage = search.PageNumber,
            ItemsPerPage = search.PerPage,
            TotalCount = totalCount,
            PagesCount = (int)Math.Ceiling(totalCount / (double)search.PerPage),
            Data = data
        };
    }

    public int Id => 21;
    public string Name => "Get all shipping methods";
}
