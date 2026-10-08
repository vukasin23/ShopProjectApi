using ShopProject.Application;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.Application.Searches;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetInventoriesQuery:IGetInventoriesQuery
{
    private readonly ShopProjectContext _context;
    
    public EfGetInventoriesQuery(ShopProjectContext context)
    {
        _context = context;
    }
    public PagedResponse<InventoryResponse> Execute(InventorySearch search)
    {
        var query = _context.Inventories.Where(x => !x.Product.IsDeleted);
        if (search.ProductId.HasValue)
        {
            query = query.Where(x => x.ProductId == search.ProductId);
        }

        if (search.StoreId.HasValue)
        {
            query = query.Where(x => x.StoreId == search.StoreId);
        }

        if (search.MinQuantity.HasValue)
        {
            query = query.Where(x => x.Quantity >= search.MinQuantity);
        }

        var totalCount = query.Count();

        var data = query
            .OrderBy(x => x.Id)
            .Skip((search.PageNumber - 1) * search.PerPage)
            .Take(search.PerPage)
            .Select(x => new InventoryResponse
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                StoreId = x.StoreId,
                StoreName = x.Store.Name,
                Quantity = x.Quantity,
                LastUpdated = x.LastUpdated
            })
            .ToList();

        return new PagedResponse<InventoryResponse> 
        {
            CurrentPage = search.PageNumber,
            ItemsPerPage = search.PerPage,
            TotalCount = totalCount,
            PagesCount = (int)Math.Ceiling(totalCount / (double)search.PerPage),
            Data = data
        };
    }

    public int Id => 23;
    public string Name => "Get all inventories";
}
