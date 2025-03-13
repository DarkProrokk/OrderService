namespace Infrastructure.Entity;

public class Order
{
    public int Id { get; set; }
    
    public Guid Guid{ get; set; }
    
    public Guid UserId { get; set; }
    public string Number { get; set; }
}