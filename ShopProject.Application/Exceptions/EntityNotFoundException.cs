namespace ShopProject.Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityName, int id)
        : base($"{entityName} with id {id} was not found.")
    {
    }
    
    public EntityNotFoundException(string username)
        : base($"User with this username {username} was not found.")
    {
    }
}
