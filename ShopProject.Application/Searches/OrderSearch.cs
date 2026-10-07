namespace ShopProject.Application.Searches;

public class OrderSearch:PagedSearch
{
    public DateTime? OrderDate { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? CouponId { get; set; }
    public int? ShippingMethodId { get; set; }
    public int? UserId { get; set; }
    public string? UserName { get; set; }

    public int? ProductId { get; set; }
    public string? ProductName { get; set; }


}
