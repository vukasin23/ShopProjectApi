using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject.Application
{
    public interface IApplicationActor
    {
        int Id { get; }
        string FirstName { get; }

        string LastName { get; }

        string Email { get; }

        IEnumerable<int> AllowedUseCases { get; }
    }
}
