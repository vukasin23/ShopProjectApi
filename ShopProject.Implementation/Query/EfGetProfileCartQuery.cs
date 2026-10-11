using ShopProject.Application;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetProfileCartQuery:IGetProfileCartQuery
{
    private readonly ShopProjectContext _context;
    private readonly IApplicationActor _actor;

    public EfGetProfileCartQuery(ShopProjectContext context, IApplicationActor actor)
    {
        _context = context;
        _actor = actor;
    }

    public int Id => 46;
    public string Name => "Get cart of logged in user";

    public PagedResponse<CartResponse> Execute(PagedSearch search)
    {
        var query = _context.Carts.Where(c => c.UserId == _actor.Id);

        var totalCount = query.Count();

        var data = query
            .OrderBy(c => c.Id)
            .Skip((search.PageNumber - 1) * search.PerPage)
            .Take(search.PerPage)
            .Select(c => new CartResponse
            {
                Id = c.Id,
                UserId = c.UserId,
                User = new UserResponse
                {
                    Id = c.User.Id,
                    Username = c.User.Username,
                    FirstName = c.User.FirstName,
                    LastName = c.User.LastName
                },
                CartItems = c.CartItems.Select(ci => new CartItemResponse
                {
                    Id = ci.Id,
                    AddedAt = ci.AddedAt,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity
                }).ToList(),
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            })
            .ToList();

        return new PagedResponse<CartResponse>
        {
            CurrentPage = search.PageNumber,
            ItemsPerPage = search.PerPage,
            TotalCount = totalCount,
            PagesCount = (int)Math.Ceiling(totalCount / (double)search.PerPage),
            Data = data
        };
    }
}
