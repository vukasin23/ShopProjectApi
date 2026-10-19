using ShopProject.Application.DataTransfer;

namespace ShopProject.Application.Responses;

public class OrderResponse
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public UserResponse User { get; set; }
    public List<OrderLineResponse> OrderLines { get; set; }
    public CouponDto Coupon { get; set; }
    public ShippingMethodDto  ShippingMethod { get; set; }
    public decimal TotalPrice { get; set; }
}