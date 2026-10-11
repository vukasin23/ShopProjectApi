using ShopProject.Application.DataTransfer;

namespace ShopProject.Application.Responses;

public class CartResponse
{
    public int  Id { get; set; }
    public int UserId { get; set; }
    public UserResponse User { get; set; }
    public List<CartItemResponse> CartItems { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}