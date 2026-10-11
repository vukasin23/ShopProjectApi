using Microsoft.EntityFrameworkCore;
using ShopProject.Application.Exceptions;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetProductQuery:IGetProductQuery
{
    private readonly ShopProjectContext _context;

    public EfGetProductQuery(ShopProjectContext context)
    {
        _context = context;
    }
    public ProductResponse Execute(int id)
    {
        var p = _context.Products.Include(p=>p.Category).Include(p=>p.ProductImages).Include(p=>p.ProductSpecification).Include(p=>p.Inventories).ThenInclude(p=>p.Store).FirstOrDefault(p => p.Id == id);
        if (p == null)
        {
            throw new EntityNotFoundException(nameof(Domain.Product), id);
        }

        return new ProductResponse
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Category = p.Category.Name,
            ImageUrls = p.ProductImages.Select(i => new ProductImageResponse
            {
                Id = i.Id,
                ProductId = i.ProductId,
                ImageUrl = i.ImageUrl,
                AltText = i.AltText,
                IsPrimary = i.IsPrimary
            }).ToList(),
            ProductSpecifications = p.ProductSpecification.Select(s => new ProductSpecificationResponse
            {
                Id = s.Id,
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
        };
    }
    

    public int Id => 45;
    public string Name => "Get product by name";
}