namespace ShopProject.Application.Searches;

public class CategorySearch:PagedSearch
{
    public string? Name { get; set; }
    public int? ParentId { get; set; }
}
