namespace ShopProject.Application.Searches;

public class CouponSearch:PagedSearch
{
    public string? Code { get; set; }
    public bool OnlyValid { get; set; }
}
