using Microsoft.EntityFrameworkCore;
using ShopProject.Application;
using ShopProject.Application.DataTransfer;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.Application.Searches;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetCartsQuery:IGetCartsQuery
{
    private readonly ShopProjectContext _context;
    public EfGetCartsQuery(ShopProjectContext context)
    {
        _context = context;
    }
    public int Id => 20;

    public string Name => "Get all carts";

    public PagedResponse<CartResponse> Execute(CartSearch search)
    {
        var query = _context.Carts.Include(c=>c.CartItems).AsQueryable();
        if (search.CartId > 0)
        {
            query.Where(c=>c.Id == search.CartId);
        }
        if (search.UserId > 0)
        {
            query.Where(c => c.UserId == search.UserId);
        }

        if (search.CartItemsCount > 0)
        {
            query.Where(c=>c.CartItems.Count >=search.CartItemsCount);
        }
        
        var totalCount = query.Count();

        var data = query
            .OrderBy(x => x.Id)
            .Skip((search.PageNumber - 1) * search.PerPage)
            .Take(search.PerPage)
            .Select(x => new CartResponse
            {
               Id = x.Id,
               UserId = x.UserId,
               CartItems = x.CartItems.Select(ci=>new CartItemDto
               {
                   AddedAt = ci.AddedAt,
                   ProductId = ci.ProductId,
                   Quantity = ci.Quantity
                   
               }).ToList(), 
               CreatedAt = x.CreatedAt,
               UpdatedAt = x.UpdatedAt,
                
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