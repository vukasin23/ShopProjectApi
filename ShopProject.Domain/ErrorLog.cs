namespace ShopProject.Domain;

public class ErrorLog
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public string Username { get; set; }
    public string StackTrace { get; set; }
    public string ExceptionType { get; set; }
    
    public DateTime OccurredAt { get; set; }
}