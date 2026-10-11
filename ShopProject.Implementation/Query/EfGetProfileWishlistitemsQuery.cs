using ShopProject.Application;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetProfileWishlistitemsQuery:IGetProfileWishlistitemsQuery
{
    private readonly ShopProjectContext _context;
    private readonly IApplicationActor _actor;

    public EfGetProfileWishlistitemsQuery(ShopProjectContext context, IApplicationActor actor)
    {
        _context = context;
        _actor = actor;
    }

    public int Id => 48;
    public string Name => "Get wishlist items of logged in user";

    public PagedResponse<WishlistItemResponse> Execute(PagedSearch search)
    {
        var query = _context.WishlistItems.Where(w => w.UserId == _actor.Id);

        var totalCount = query.Count();

        var data = query
            .OrderBy(w => w.Id)
            .Skip((search.PageNumber - 1) * search.PerPage)
            .Take(search.PerPage)
            .Select(w => new WishlistItemResponse
            {
                Id = w.Id,
                ProductId = w.Product.Id,
                ProductName = w.Product.Name,
                CategoryId = w.Product.CategoryId,
                CategoryName = w.Product.Category.Name,
                UserId = w.User.Id,
                UserName = w.User.Username
            })
            .ToList();

        return new PagedResponse<WishlistItemResponse>
        {
            CurrentPage = search.PageNumber,
            ItemsPerPage = search.PerPage,
            TotalCount = totalCount,
            PagesCount = (int)Math.Ceiling(totalCount / (double)search.PerPage),
            Data = data
        };
    }
}
