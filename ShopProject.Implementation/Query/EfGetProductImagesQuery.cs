using ShopProject.Application;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.Application.Searches;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetProductImagesQuery:IGetProductImagesQuery
{
    private readonly ShopProjectContext _context;

    public EfGetProductImagesQuery(ShopProjectContext context)
    {
        _context = context;
    }

    public int Id => 49;
    public string Name => "Get all product images";

    public PagedResponse<ProductImageResponse> Execute(ProductImageSearch search)
    {
        var query = _context.ProductImages.Where(x => !x.Product.IsDeleted);

        if (search.ProductId.HasValue)
        {
            query = query.Where(x => x.ProductId == search.ProductId);
        }

        if (search.IsPrimary.HasValue)
        {
            query = query.Where(x => x.IsPrimary == search.IsPrimary);
        }

        var totalCount = query.Count();

        var data = query
            .OrderBy(x => x.Id)
            .Skip((search.PageNumber - 1) * search.PerPage)
            .Take(search.PerPage)
            .Select(x => new ProductImageResponse
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ImageUrl = x.ImageUrl,
                AltText = x.AltText,
                IsPrimary = x.IsPrimary
            })
            .ToList();

        return new PagedResponse<ProductImageResponse>
        {
            CurrentPage = search.PageNumber,
            ItemsPerPage = search.PerPage,
            TotalCount = totalCount,
            PagesCount = (int)Math.Ceiling(totalCount / (double)search.PerPage),
            Data = data
        };
    }
}
