using ShopProject.Application.Responses;
using ShopProject.Application.Searches;

namespace ShopProject.Application.Query;

public interface IGetShippingMethodsQuery:IQuery<ShippingMethodSearch, ShippingMethodResponse>
{
    
}
