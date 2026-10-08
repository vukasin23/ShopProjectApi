using ShopProject.Application.Responses;

namespace ShopProject.Application.Query;

public interface IGetUserQuery:IGetByOne<UserResponse, string>
{
    
}