using ShopProject.Application;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.Application.Searches;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetUsersQuery:IGetUsersQuery
{
    private readonly ShopProjectContext _context;

    public EfGetUsersQuery(ShopProjectContext context)
    {
        _context = context;
    }

    public int Id => 53;
    public string Name => "Get all users";

    public PagedResponse<UserResponse> Execute(UserSearch search)
    {
        var query = _context.Users.Where(x => !x.IsDeleted);

        if (!string.IsNullOrWhiteSpace(search.Username))
        {
            query = query.Where(x => x.Username.Contains(search.Username));
        }

        if (!string.IsNullOrWhiteSpace(search.FirstName))
        {
            query = query.Where(x => x.FirstName.Contains(search.FirstName));
        }

        if (!string.IsNullOrWhiteSpace(search.LastName))
        {
            query = query.Where(x => x.LastName.Contains(search.LastName));
        }

        var totalCount = query.Count();

        var data = query
            .OrderBy(x => x.Id)
            .Skip((search.PageNumber - 1) * search.PerPage)
            .Take(search.PerPage)
            .Select(x => new UserResponse
            {
                Id = x.Id,
                Username = x.Username,
                FirstName = x.FirstName,
                LastName = x.LastName
            })
            .ToList();

        return new PagedResponse<UserResponse>
        {
            CurrentPage = search.PageNumber,
            ItemsPerPage = search.PerPage,
            TotalCount = totalCount,
            PagesCount = (int)Math.Ceiling(totalCount / (double)search.PerPage),
            Data = data
        };
    }
}
