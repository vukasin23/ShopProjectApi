using System.Text.Json.Serialization;

namespace ShopProject.Application.DataTransfer;

public class UpdateAddressDto
{
    // Id comes from the route, so it is hidden from the request body.
    [JsonIgnore]
    public int Id { get; set; }

    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
}
