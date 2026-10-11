using ShopProject.Application;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.Application.Searches;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetProductSpecificationsQuery:IGetProductSpecificationsQuery
{
    private readonly ShopProjectContext _context;

    public EfGetProductSpecificationsQuery(ShopProjectContext context)
    {
        _context = context;
    }

    public int Id => 51;
    public string Name => "Get all product specifications";

    public PagedResponse<ProductSpecificationResponse> Execute(ProductSpecificationSearch search)
    {
        var query = _context.ProductSpecifications.Where(x => !x.Product.IsDeleted);

        if (search.ProductId.HasValue)
        {
            query = query.Where(x => x.ProductId == search.ProductId);
        }

        if (!string.IsNullOrWhiteSpace(search.SpecificationName))
        {
            query = query.Where(x => x.SpecificationName.Contains(search.SpecificationName));
        }

        if (!string.IsNullOrWhiteSpace(search.SpecificationValue))
        {
            query = query.Where(x => x.SpecificationValue.Contains(search.SpecificationValue));
        }

        var totalCount = query.Count();

        var data = query
            .OrderBy(x => x.Id)
            .Skip((search.PageNumber - 1) * search.PerPage)
            .Take(search.PerPage)
            .Select(x => new ProductSpecificationResponse
            {
                Id = x.Id,
                ProductId = x.ProductId,
                SpecificationName = x.SpecificationName,
                SpecificationValue = x.SpecificationValue
            })
            .ToList();

        return new PagedResponse<ProductSpecificationResponse>
        {
            CurrentPage = search.PageNumber,
            ItemsPerPage = search.PerPage,
            TotalCount = totalCount,
            PagesCount = (int)Math.Ceiling(totalCount / (double)search.PerPage),
            Data = data
        };
    }
}
