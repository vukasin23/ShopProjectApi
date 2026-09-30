using ShopProject.Application.DataTransfer;

namespace ShopProject.Application.Responses;

public class ProductResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public IEnumerable<ProductImageResponse> ImageUrls { get; set; }
    public string Category { get; set; }
    public IEnumerable<ProductSpecificationResponse> ProductSpecifications { get; set; }
    public IEnumerable<InventoryResponse>  Inventories { get; set; }
    public IEnumerable<StoreResponse>  Stores { get; set; }
}