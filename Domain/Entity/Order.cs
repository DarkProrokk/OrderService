namespace Domain.Entity;

public class Order
{
    public Guid Guid { get; set; }
    public Guid UserId { get; set; }
    
    public List<Guid> Products { get; set; }
    
    public string Number { get; set; }

    public Order(Guid userId, List<Guid> products, string number)
    {
        Guid = Guid.NewGuid();
        UserId = userId;
        if (products.Count < 1)
        {
            throw new ArgumentException("The number of products must be greater than 0.");
        }

        Number = number;
        Products = products;
    }

    public override string ToString()
    {
        return $"Id: {Guid}, UserId: {UserId}, Products: {string.Join(", ", Products)}";
    }
}