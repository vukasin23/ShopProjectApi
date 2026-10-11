namespace ShopProject.Application.Searches;

public class ProductSpecificationSearch:PagedSearch
{
    public int? ProductId { get; set; }
    public string? SpecificationName { get; set; }
    public string? SpecificationValue { get; set; }
}
