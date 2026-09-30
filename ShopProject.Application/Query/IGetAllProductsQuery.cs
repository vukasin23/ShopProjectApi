using ShopProject.Application.DataTransfer;
using ShopProject.Application.Responses;
using ShopProject.Application.Searches;

namespace ShopProject.Application.Query;

public interface IGetAllProductsQuery:IQuery<ProductSearch, ProductResponse>
{
    
}