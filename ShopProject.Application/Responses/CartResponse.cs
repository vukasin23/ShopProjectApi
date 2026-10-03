using ShopProject.Application.DataTransfer;

namespace ShopProject.Application.Responses;

public class CartResponse
{
    public int  Id { get; set; }
    public int UserId { get; set; }
    public List<CartItemDto> CartItems { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}