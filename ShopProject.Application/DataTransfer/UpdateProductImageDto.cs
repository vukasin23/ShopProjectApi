using System.Text.Json.Serialization;

namespace ShopProject.Application.DataTransfer;

public class UpdateProductImageDto
{
    // Id comes from the route, so it is hidden from the request body.
    [JsonIgnore]
    public int Id { get; set; }

    public string ImageUrl { get; set; }
    public string AltText { get; set; }
    // Nullable, so a missing value is rejected instead of silently becoming false.
    public bool? IsPrimary { get; set; }
}
