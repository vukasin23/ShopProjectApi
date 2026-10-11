using Microsoft.EntityFrameworkCore;
using ShopProject.Application;
using ShopProject.Application.DataTransfer;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.Application.Searches;
using ShopProject.DataAccess;
using ShopProject.Domain;

namespace ShopProject.Implementation.Query;

public class EfGetWishlistitemsQuery:IGetWishlistitemsQuery
{
    
    private readonly ShopProjectContext _context;
    
    public EfGetWishlistitemsQuery(ShopProjectContext context)
    {
        _context = context;
    }

    public PagedResponse<WishlistItemResponse> Execute(WishlistitemSearch search)
    {
        var query = _context.WishlistItems.Include(w => w.Product).ThenInclude(p=>p.Category).Include(w=>w.User).AsQueryable();
        if (search.ProductId > 0)
        {
            query = query.Where(w => w.ProductId == search.ProductId);
        }
        if (search.UserId > 0)
        {
            query = query.Where(w => w.UserId == search.UserId); 
        }

        if (search.CategoryId > 0)
        {
            query = query.Where(c => c.Product.CategoryId == search.CategoryId);
        }

        if (!string.IsNullOrWhiteSpace(search.ProductName))
        {
            query = query.Where(w=>w.Product.Name.Contains(search.ProductName));
        }

        if (!string.IsNullOrWhiteSpace(search.CategoryName))
        {
            query = query.Where(w=>w.Product.Category.Name.Contains(search.CategoryName));
        }

        if (!string.IsNullOrWhiteSpace(search.UserName))
        {
            query = query.Where(w=>w.User.Username.Contains(search.UserName));
        }
        var totalCount = query.Count();
        
        var data = query.OrderBy(x=>x.Id).Skip((search.PageNumber - 1) * search.PerPage)
            .Take(search.PerPage).Select(x => new WishlistItemResponse
        {
            Id = x.Id,
            ProductId = x.Product.Id,
            ProductName = x.Product.Name,
           CategoryId = x.Product.CategoryId,
           CategoryName = x.Product.Category.Name,
           UserId = x.User.Id,
           UserName = x.User.Username,
        }).ToList();
        
        return new PagedResponse<WishlistItemResponse>
        {
            CurrentPage = search.PageNumber,
            Data = data,
            ItemsPerPage = search.PerPage,
            PagesCount = (int)Math.Ceiling(totalCount / (double)search.PerPage),
            TotalCount = totalCount
        };
    }

    public int Id => 43;
    public string Name => "Get all wishlist items";
}