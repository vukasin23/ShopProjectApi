namespace ShopProject.Application.Responses;

public class InventoryResponse
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public int Quantity { get; set; }
    public DateTime LastUpdated { get; set; }
}
