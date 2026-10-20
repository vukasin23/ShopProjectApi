using ShopProject.Application;
using ShopProject.DataAccess;
using ShopProject.Domain;

namespace ShopProject.Implementation.Logging.Errors;

public class EfErrorLogger:IErrorLogger
{
    private readonly ShopProjectContext _context;
    public EfErrorLogger(ShopProjectContext context)
    {
        _context = context;
    }
    public Guid Log(Exception ex, IApplicationActor actor)
    {
        var error = new ErrorLog
        {
            Id = Guid.NewGuid(),
            Message = ex.Message,
            StackTrace = ex.StackTrace,
            ExceptionType = ex.GetType().Name,
            Username = actor.FirstName + " " + actor.LastName,
            OccurredAt = DateTime.UtcNow
        };
        
        _context.ErrorLogs.Add(error);
        _context.SaveChanges();

        return error.Id;
    }
}