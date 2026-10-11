namespace ShopProject.Application.Searches;

public class WishlistitemSearch:PagedSearch
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public int ProductId { get; set; }
    public string CategoryName { get; set; }
    public int CategoryId { get; set; }
    public string UserName { get; set; }
    public int UserId { get; set; }
}