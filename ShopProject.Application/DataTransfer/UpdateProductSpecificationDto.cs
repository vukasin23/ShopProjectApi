using System.Text.Json.Serialization;

namespace ShopProject.Application.DataTransfer;

public class UpdateProductSpecificationDto
{
    // Id comes from the route, so it is hidden from the request body.
    [JsonIgnore]
    public int Id { get; set; }

    public string SpecificationName { get; set; }
    public string SpecificationValue { get; set; }
}
