using Domain.Exceptions;

namespace Domain.Entity;

public class Order
{
    public Guid Guid { get; set; }
    public Guid UserId { get; set; }
    
    public List<Guid> Products { get; set; }
    
    public string Number { get; set; }

    private Order(Guid userId, List<Guid> products, string number) 
    {
        Guid = Guid.NewGuid();
        if (userId == default) throw new OrderCreateArgumentException("User Guid cannot be default");
        UserId = userId;
        if (products.Count < 1) throw new OrderCreateArgumentException("The number of products must be greater than 0.");
        Number = number;
        Products = products;
    }

    public static Order Create(Guid userGuid, List<Guid> productsList, string number) => new Order(userGuid, productsList, number);

    public override string ToString()
    {
        return $"Id: {Guid}, UserId: {UserId}, Products: {string.Join(", ", Products)}";
    }
}