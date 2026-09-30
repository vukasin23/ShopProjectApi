namespace ShopProject.Application.Responses;

public class InventoryResponse
{
    public int ProductId { get; set; }
    public int StoreId { get; set; }
    public int Quantity { get; set; }
    public DateTime LastUpdated { get; set; }
}
