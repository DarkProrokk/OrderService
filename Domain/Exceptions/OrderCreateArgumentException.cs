namespace Domain.Exceptions;

public class OrderCreateArgumentException: ArgumentException
{
    public OrderCreateArgumentException(string message) : base(message)
    {
        
    }

    public OrderCreateArgumentException()
    {
        
    }
}