using ShopProject.Application;
using ShopProject.Application.Query;
using ShopProject.Application.Responses;
using ShopProject.Application.Searches;
using ShopProject.DataAccess;

namespace ShopProject.Implementation.Query;

public class EfGetAddressesQuery:IGetAddressesQuery
{
    private readonly ShopProjectContext _context;
    
    public EfGetAddressesQuery(ShopProjectContext context)
    {
        _context = context;
    }
    public PagedResponse<AddressResponse> Execute(AddressSearch search)
    {
        var query = _context.Addresses.AsQueryable();
        if (search.UserId.HasValue)
        {
            query = query.Where(x => x.UserId == search.UserId);
        }

        if (!string.IsNullOrEmpty(search.City))
        {
            query = query.Where(x => x.City.Contains(search.City));
        }

        if (!string.IsNullOrEmpty(search.Country))
        {
            query = query.Where(x => x.Country.Contains(search.Country));
        }

        var totalCount = query.Count();

        var data = query
            .OrderBy(x => x.Id)
            .Skip((search.PageNumber - 1) * search.PerPage)
            .Take(search.PerPage)
            .Select(x => new AddressResponse
            {
                Id = x.Id,
                UserId = x.UserId,
                User = new UserResponse
                {
                    Id = x.User.Id,
                    Username = x.User.Username,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName
                },
                Street = x.Street,
                City = x.City,
                State = x.State,
                ZipCode = x.ZipCode,
                Country = x.Country,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .ToList();

        return new PagedResponse<AddressResponse> 
        {
            CurrentPage = search.PageNumber,
            ItemsPerPage = search.PerPage,
            TotalCount = totalCount,
            PagesCount = (int)Math.Ceiling(totalCount / (double)search.PerPage),
            Data = data
        };
    }

    public int Id => 22;
    public string Name => "Get all addresses";
}
