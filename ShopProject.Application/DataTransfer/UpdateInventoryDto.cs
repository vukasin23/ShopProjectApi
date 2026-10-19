using System.Text.Json.Serialization;

namespace ShopProject.Application.DataTransfer;

public class UpdateInventoryDto
{
    // Id comes from the route, so it is hidden from the request body.
    [JsonIgnore]
    public int Id { get; set; }

    public int Quantity { get; set; }
}
