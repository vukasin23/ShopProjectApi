using ShopProject.Application.Responses;

namespace ShopProject.Application.Query;

public interface IGetProductSpecificationQuery:IGetByOne<ProductSpecificationResponse, int>
{
    
}
