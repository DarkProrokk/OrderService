using Domain.Entity;
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
        var productIds = new List<Guid> { productId };

        //Act
        var order = new Order(userGuid, productIds, "asd");
        
        //Assert
        Assert.NotNull(order);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenProductsListIsEmpty()
    {
        // Arrange
        var userGuid = NewGuid();
        var productIds = new List<Guid>();
        
        //Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new Order(userGuid, productIds, "asd"));
        Assert.Equal("The number of products must be greater than 0.", exception.Message);
    }

    [Fact]
    public void ToString_ShouldReturnCorrectItemInfo()
    {
        // Arrange
        var userGuid = NewGuid();
        var productId = NewGuid();
        var productIds = new List<Guid> { productId };

        //Act
        var order = new Order(userGuid, productIds, "asd");
        
        //Assert
        Assert.Equal($"Id: {order.Guid}, UserId: {userGuid}, Products: {string.Join(", ", productIds)}", order.ToString());
    }
}