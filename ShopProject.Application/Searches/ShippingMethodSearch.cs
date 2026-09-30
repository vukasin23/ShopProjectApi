namespace ShopProject.Application.Searches;

public class ShippingMethodSearch:PagedSearch
{
    public string? Name { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}
