namespace ShopProject.Application.Searches;

public class ProductSearch:PagedSearch
{
    public string? Name { get; set; }
    public string? SpecificationName { get; set; }
    public string? SpecificationValue { get; set; }
    public int CategoryId { get; set; }
    public int MinPrice { get; set; }
    public int MaxPrice { get; set; }

    public bool IsAvailable { get; set; }
}