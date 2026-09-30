namespace ShopProject.Application.Searches;

public class AddressSearch:PagedSearch
{
    public int? UserId { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
}
