
using ShopProject.Application;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.Application.Searches;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetProductsQuery:IGetAllProductsQuery
{
    private readonly ShopProjectContext _context;
    
    public EfGetProductsQuery(ShopProjectContext context)
    {
        _context = context;
    }
    public PagedResponse<ProductResponse> Execute(ProductSearch search)
    {
        var query =  _context.Products.AsQueryable();
        if (search.CategoryId > 0)
        {
            query = query.Where(p => p.CategoryId == search.CategoryId);
        }

        if (!string.IsNullOrEmpty(search.Name))
        {
            query = query.Where(p => p.Name.Contains(search.Name));
        }

        if (search.MinPrice > 0)
        {
            query = query.Where(p => p.Price >= search.MinPrice);
        }

        if (search.MaxPrice > 0 && search.MaxPrice > search.MinPrice)
        {
            query = query.Where(p => p.Price <= search.MaxPrice);
        }

        if (!string.IsNullOrEmpty(search.SpecificationName))
        {
            query = query.Where(p => p.ProductSpecification.Any(ps => ps.SpecificationName.Contains(search.SpecificationName)));
        }
        if (!string.IsNullOrEmpty(search.SpecificationValue))
        {
            query = query.Where(p => p.ProductSpecification.Any(ps => ps.SpecificationValue.Contains(search.SpecificationValue)));
        }

        query = query.Where(p => p.Inventories.Any(i => i.Quantity > 0));

        var totalCount = query.Count();

        var data = query
            .OrderBy(p => p.Id)
            .Skip((search.PageNumber - 1) * search.PerPage)
            .Take(search.PerPage)
            .Select(p => new ProductResponse
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Category = p.Category.Name,
                ImageUrls = p.ProductImages.Select(i => new ProductImageResponse
                {
                    ProductId = i.ProductId,
                    ImageUrl = i.ImageUrl,
                    AltText = i.AltText,
                    IsPrimary = i.IsPrimary
                }).ToList(),
                ProductSpecifications = p.ProductSpecification.Select(s => new ProductSpecificationResponse
                {
                    ProductId = s.ProductId,
                    SpecificationName = s.SpecificationName,
                    SpecificationValue = s.SpecificationValue
                }).ToList(),
                Inventories = p.Inventories.Where(i => i.Quantity > 0).Select(i => new InventoryResponse
                {
                    ProductId = i.ProductId,
                    StoreId = i.StoreId,
                    Quantity = i.Quantity,
                    LastUpdated = i.LastUpdated
                }).ToList(),
                Stores = p.Inventories.Where(i => i.Quantity > 0).Select(i => new StoreResponse
                {
                    Id = i.Store.Id,
                    Name = i.Store.Name,
                    Description = i.Store.Description,
                    City = i.Store.City,
                    Address = i.Store.Address,
                    Phone = i.Store.Phone,
                    IsActive = i.Store.IsActive
                }).ToList()
            })
            .ToList();

        return new PagedResponse<ProductResponse> 
        {
            CurrentPage = search.PageNumber,
            ItemsPerPage = search.PerPage,
            TotalCount = totalCount,
            PagesCount = (int)Math.Ceiling(totalCount / (double)search.PerPage),
            Data = data
        };
    }

    public int Id => 17;
    public string Name => "Get all products";
}