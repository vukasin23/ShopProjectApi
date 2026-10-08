using ShopProject.Application.Responses;

namespace ShopProject.Application.Query;

public interface IGetCategoryQuery : IGetByIdQuery<CategoryResponse>
{
}
