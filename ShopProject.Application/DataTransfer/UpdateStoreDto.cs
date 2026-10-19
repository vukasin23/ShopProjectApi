using System.Text.Json.Serialization;

namespace ShopProject.Application.DataTransfer;

public class UpdateStoreDto
{
    // Id comes from the route, so it is hidden from the request body.
    [JsonIgnore]
    public int Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }

    // Nullable, so a missing value is rejected instead of silently becoming false.
    public bool? IsActive { get; set; }
}
