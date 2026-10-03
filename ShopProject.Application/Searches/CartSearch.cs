namespace ShopProject.Application.Searches;

public class CartSearch:PagedSearch
{
    public int CartId { get; set; }
    public int CartItemsCount { get; set; }
    public int UserId { get; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}