using ShopProject.Application;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.Application.Searches;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetCouponsQuery:IGetCouponsQuery
{
    private readonly ShopProjectContext _context;
    
    public EfGetCouponsQuery(ShopProjectContext context)
    {
        _context = context;
    }
    public PagedResponse<CouponResponse> Execute(CouponSearch search)
    {
        var query = _context.Coupons.AsQueryable();
        if (!string.IsNullOrEmpty(search.Code))
        {
            query = query.Where(x => x.Code.Contains(search.Code));
        }

        if (search.OnlyValid)
        {
            query = query.Where(x => x.ExpiryDate > DateTime.UtcNow);
        }

        var totalCount = query.Count();

        var data = query
            .OrderBy(x => x.Id)
            .Skip((search.PageNumber - 1) * search.PerPage)
            .Take(search.PerPage)
            .Select(x => new CouponResponse
            {
                Id = x.Id,
                Code = x.Code,
                DiscountAmount = x.DiscountAmount,
                ExpiryDate = x.ExpiryDate
            })
            .ToList();

        return new PagedResponse<CouponResponse> 
        {
            CurrentPage = search.PageNumber,
            ItemsPerPage = search.PerPage,
            TotalCount = totalCount,
            PagesCount = (int)Math.Ceiling(totalCount / (double)search.PerPage),
            Data = data
        };
    }

    public int Id => 18;
    public string Name => "Get all coupons";
}
