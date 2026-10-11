namespace ShopProject.Application.Searches;

public class ProductImageSearch:PagedSearch
{
    public int? ProductId { get; set; }
    public bool? IsPrimary { get; set; }
}
