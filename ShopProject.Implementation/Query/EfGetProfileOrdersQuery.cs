using ShopProject.Application;
using ShopProject.Application.DataTransfer;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetProfileOrdersQuery:IGetProfileOrdersQuery
{
    private readonly ShopProjectContext _context;
    private readonly IApplicationActor _actor;

    public EfGetProfileOrdersQuery(ShopProjectContext context, IApplicationActor actor)
    {
        _context = context;
        _actor = actor;
    }

    public int Id => 47;
    public string Name => "Get orders of logged in user";

    public PagedResponse<OrderResponse> Execute(PagedSearch search)
    {
        var query = _context.Orders.Where(o => o.UserId == _actor.Id);

        var totalCount = query.Count();

        var data = query
            .OrderBy(o => o.Id)
            .Skip((search.PageNumber - 1) * search.PerPage)
            .Take(search.PerPage)
            .Select(o => new OrderResponse
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                User = new UserResponse
                {
                    Id = o.User.Id,
                    Username = o.User.Username,
                    FirstName = o.User.FirstName,
                    LastName = o.User.LastName
                },
                TotalPrice = o.TotalPrice,
                OrderLines = o.OrderLines.Select(ol => new OrderLineResponse
                {
                    Id = ol.Id,
                    ProductId = ol.ProductId,
                    Quantity = ol.Quantity,
                    UnitPrice = ol.UnitPrice,
                }).ToList(),
                Coupon = o.Coupon == null ? null : new CouponDto
                {
                    Code = o.Coupon.Code,
                    DiscountAmount = (int)o.Coupon.DiscountAmount,
                    ExpiryDate = o.Coupon.ExpiryDate
                },
                ShippingMethod = new ShippingMethodDto
                {
                    Name = o.ShippingMethod.Name,
                    Price = (int)o.ShippingMethod.Price,
                    Description = o.ShippingMethod.Description
                }
            })
            .ToList();

        return new PagedResponse<OrderResponse>
        {
            CurrentPage = search.PageNumber,
            ItemsPerPage = search.PerPage,
            TotalCount = totalCount,
            PagesCount = (int)Math.Ceiling(totalCount / (double)search.PerPage),
            Data = data
        };
    }
}
