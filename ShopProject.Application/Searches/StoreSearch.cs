namespace ShopProject.Application.Searches;

public class StoreSearch:PagedSearch
{
    public string? Name { get; set; }
    public string? City { get; set; }
    public bool? IsActive { get; set; }
}
