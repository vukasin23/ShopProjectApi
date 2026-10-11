using ShopProject.Application.Responses;

namespace ShopProject.Application.Query;

public interface IGetProductImageQuery:IGetByOne<ProductImageResponse, int>
{
    
}
