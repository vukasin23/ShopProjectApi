namespace ShopProject.Application.Responses;

public class ProductSpecificationResponse
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string SpecificationName { get; set; }
    public string SpecificationValue { get; set; }
}
