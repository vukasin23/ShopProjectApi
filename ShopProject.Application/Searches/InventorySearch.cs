namespace ShopProject.Application.Searches;

public class InventorySearch:PagedSearch
{
    public int? ProductId { get; set; }
    public int? StoreId { get; set; }
    public int? MinQuantity { get; set; }
}
