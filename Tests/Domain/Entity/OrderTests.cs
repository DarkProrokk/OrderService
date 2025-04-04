using Domain.Entity;
using Domain.Exceptions;
using static System.Guid;

namespace Tests.Domain.Entity;

public class OrderTests
{
    [Fact]
    public void Constructor_ShouldNotBeNull()
    {
        // Arrange
        var userGuid = NewGuid();
        var productId = NewGuid();
        var productIds = new Dictionary<Guid, int> {{Guid.NewGuid(), 5}, { Guid.NewGuid(), 4 }};

        //Act
        var order = Order.Create(userGuid, productIds, "asd");
        
        //Assert
        Assert.NotNull(order);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenProductsListIsEmpty()
    {
        // Arrange
        var userGuid = NewGuid();
        var productIds = new Dictionary<Guid, int>();
        
        //Act & Assert
        var exception = Assert.Throws<OrderCreateArgumentException>(() => Order.Create(userGuid, productIds, "asd"));
        Assert.Equal("The number of products must be greater than 0.", exception.Message);
    }

    [Fact]
    public void ToString_ShouldReturnCorrectItemInfo()
    {
        // Arrange
        var userGuid = NewGuid();
        var productId = NewGuid();
        var productIds = new Dictionary<Guid, int> {{Guid.NewGuid(), 5}, { Guid.NewGuid(), 4 }};

        //Act
        var order = Order.Create(userGuid, productIds, "asd");
        
        //Assert
        Assert.Equal($"Id: {order.Guid}, UserId: {userGuid}, Products: {string.Join(", ", productIds)}", order.ToString());
    }
    
}