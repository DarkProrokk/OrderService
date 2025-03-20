using Application.Commands;
using Application.Handlers;
using Domain.Entity;
using Domain.Interfaces;
using Domain.Repository;
using Moq;
using Tests.Fixture;

namespace Tests.Application.Handlers;

public class CreateOrderCommandHandlerTests: CreateOrderCommandHandlerTestBase
{
    [Fact]
    public async Task Handle_ShouldCreateOrderAndReturnGuid()
    {
        //Arrange
        var userGuid = Guid.NewGuid();
        var productList = new List<Guid> {Guid.NewGuid(), Guid.NewGuid()};
        var orderNumber = "2025-0001";
        var expectedOrderGuid = Guid.NewGuid();
        var orderArrange = Order.Create(userGuid, productList, orderNumber);

        MockOrderFactory
            .Setup(f => f.CreateAsync(userGuid, productList))
            .ReturnsAsync(orderArrange);

        MockOrderRepository
            .Setup(r => r.AddAsync(It.IsAny<Order>()))
            .Callback<Order>(order =>
            {
                order.Guid = expectedOrderGuid;
            });

        var handler = new CreateOrderCommandHandler(MockOrderRepository.Object, MockUnitOfWork.Object, MockOrderFactory.Object);

        var command = new CreateOrderCommand(userGuid, productList);
        
        //Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        //Assrt
        Assert.Equal(expectedOrderGuid, result);
        
        MockOrderRepository.Verify(r => r.AddAsync(It.Is<Order>(o =>
            o.UserId == userGuid &&
            o.Products.SequenceEqual(productList) &&
            o.Number == orderNumber)), Times.Once);
        
        MockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
    }
    
    
    [Fact]
    public async Task Handle_WithEmptyProductListShouldReturnException()
    {
        //Arrange
        var userGuid = Guid.NewGuid();
        var productList = new List<Guid> ();

        MockOrderFactory
            .Setup(f => f.CreateAsync(userGuid, productList))
            .Throws<ArgumentException>();

        MockOrderRepository
            .Setup(r => r.AddAsync(It.IsAny<Order>()));


        var handler = new CreateOrderCommandHandler(MockOrderRepository.Object, MockUnitOfWork.Object, MockOrderFactory.Object);

        var command = new CreateOrderCommand(userGuid, productList);
        
        //Act and Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => await handler.Handle(command, CancellationToken.None));
        
        MockOrderRepository.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Never);
        
        MockUnitOfWork.Verify(u => u.SaveAsync(), Times.Never);
    }
}