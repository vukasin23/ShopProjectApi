using ShopProject.Application.Responses;

namespace ShopProject.Application.Query;

public interface IGetProductQuery:IGetByOne<ProductResponse, int>
{
    
}