using FluentValidation;
using ShopProject.Application;
using ShopProject.Application.Command;
using ShopProject.Application.DataTransfer;
using ShopProject.Application.Exceptions;
using ShopProject.DataAccess;
using ShopProject.Implementation.Validators;

namespace ShopProject.Implementation.Command;

public class EfUpdateUserCommand : IUpdateUserCommand
{
    private readonly ShopProjectContext _context;
    private readonly UpdateUserValidator _validator;
    private readonly IApplicationActor _actor;

    public EfUpdateUserCommand(ShopProjectContext context, UpdateUserValidator validator, IApplicationActor actor)
    {
        _context = context;
        _validator = validator;
        _actor = actor;
    }

    public int Id => 65;
    public string Name => "Update user";

    public void Execute(UpdateUserDto request)
    {
        // A user can change only their own account. Deleted users are excluded by hand, there is no query filter for them.
        var user = _context.Users.FirstOrDefault(u => u.Id == request.Id && u.Id == _actor.Id && !u.IsDeleted)
            ?? throw new EntityNotFoundException(nameof(Domain.User), request.Id);

        _validator.ValidateAndThrow(request);

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Username = request.Username;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;

        _context.SaveChanges();
    }
}
