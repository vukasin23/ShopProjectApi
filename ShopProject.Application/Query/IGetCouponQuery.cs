using ShopProject.Application.Responses;

namespace ShopProject.Application.Query;

public interface IGetCouponQuery:IGetByOne<CouponResponse, int>
{
    
}
