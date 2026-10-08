using ShopProject.Application.Responses;

namespace ShopProject.Application.Query;

public interface IGetInventoryQuery:IGetByOne<InventoryResponse, int>
{
    
}
