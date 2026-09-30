using ShopProject.Application.Responses;
using ShopProject.Application.Searches;

namespace ShopProject.Application.Query;

public interface IGetCategoriesQuery:IQuery<CategorySearch, CategoryResponse>
{
    
}
