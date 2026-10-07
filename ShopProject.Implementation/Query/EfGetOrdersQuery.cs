using FluentValidation;
using ShopProject.Application;
using ShopProject.Application.DataTransfer;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.Application.Searches;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;

namespace ShopProject.Implementation.Query;

public class EfGetOrdersQuery:IGetOrdersQuery
{
    private readonly ShopProjectContext _context;
    private readonly OrderSearchValidator _validator;
    public EfGetOrdersQuery(ShopProjectContext context, OrderSearchValidator validator)
    {
        _context = context;
        _validator = validator;
    }

    public int Id => 35;
    public string Name => "Get all orders";

    public PagedResponse<OrderResponse> Execute(OrderSearch search)
    {
        _validator.ValidateAndThrow(search);

        var query = _context.Orders.AsQueryable();

        if (search.OrderDate.HasValue)
        {
            var date = search.OrderDate.Value.Date;
            query = query.Where(o => o.OrderDate.Date == date);
        }

        if (search.MinPrice.HasValue)
        {
            query = query.Where(o => o.TotalPrice >= search.MinPrice.Value);
        }

        if (search.MaxPrice.HasValue)
        {
            query = query.Where(o => o.TotalPrice <= search.MaxPrice.Value);
        }

        if (search.CouponId.HasValue)
        {
            query = query.Where(o => o.CouponId == search.CouponId.Value);
        }

        if (search.ShippingMethodId.HasValue)
        {
            query = query.Where(o => o.ShippingMethodId == search.ShippingMethodId.Value);
        }

        if (search.UserId.HasValue)
        {
            query = query.Where(o => o.UserId == search.UserId.Value);
        }

        if (!string.IsNullOrWhiteSpace(search.UserName))
        {
            query = query.Where(o => o.User.Username.Contains(search.UserName));
        }

        if (search.ProductId.HasValue)
        {
            query = query.Where(o => o.OrderLines.Any(ol => ol.ProductId == search.ProductId.Value));
        }

        if (!string.IsNullOrWhiteSpace(search.ProductName))
        {
            query = query.Where(o => o.OrderLines.Any(ol => ol.Product.Name.Contains(search.ProductName)));
        }

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
                OrderLines = o.OrderLines.Select(ol => new OrderLineDto
                {
                    OrderId = ol.OrderId,
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
