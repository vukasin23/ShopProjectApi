namespace ShopProject.Application.DataTransfer;

public class OrderDto
{
    public int? CouponId { get; set; }
    public int ShippingMethodId { get; set; }
    public int AddressId { get; set; }

}