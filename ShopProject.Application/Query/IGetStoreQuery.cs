using ShopProject.Application.Responses;

namespace ShopProject.Application.Query;

public interface IGetStoreQuery:IGetByOne<StoreResponse, int>
{
    
}
