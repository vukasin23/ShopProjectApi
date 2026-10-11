using ShopProject.Application.Responses;

namespace ShopProject.Application.Query;

public interface IGetProfileCartQuery:IQuery<PagedSearch, CartResponse>
{
    
}
