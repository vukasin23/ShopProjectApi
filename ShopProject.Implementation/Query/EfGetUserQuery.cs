using ShopProject.Application.Exceptions;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetUserQuery:IGetUserQuery
{
    private readonly ShopProjectContext  _context;

    public EfGetUserQuery(ShopProjectContext context)
    {
        _context = context;
    }

    public UserResponse Execute(string username)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username.Equals(username));
        Console.WriteLine(user);
        if (user == null)
        {
            throw new EntityNotFoundException(username);
        }

        return new UserResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username
        };
    }

    public int Id => 42;
    public string Name => "Get user profile";
}