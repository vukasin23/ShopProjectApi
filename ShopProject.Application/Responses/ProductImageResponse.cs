namespace ShopProject.Application.Responses;

public class ProductImageResponse
{
    public int ProductId { get; set; }
    public string ImageUrl { get; set; }
    public string AltText { get; set; }
    public bool IsPrimary { get; set; }
}
