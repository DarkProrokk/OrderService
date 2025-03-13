namespace Domain.Entity;

public class Order
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public List<Guid> Products { get; set; }

    public Order(Guid userId, List<Guid> products)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        if (products.Count < 1)
        {
            throw new ArgumentException("The number of products must be greater than 0.");
        }
        Products = products;
    }

    public override string ToString()
    {
        return $"Id: {Id}, UserId: {UserId}, Products: {string.Join(", ", Products)}";
    }
}