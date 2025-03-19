using Application.Commands;
using Application.Handlers;
using Domain.Entity;
using Domain.Interfaces;
using Domain.Repository;
using Moq;

namespace Tests.Application.Handlers;

public class CreateOrderCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateOrderAndReturnGuid()
    {
        //Arange
        var mockOrderRepository = new Mock<IOrderRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockOrderNumberGenerator = new Mock<IOrderNumberGenerator>();

        var userGuid = Guid.NewGuid();
        var productList = new List<Guid> {Guid.NewGuid(), Guid.NewGuid()};
        var generatedOrderNumber = "ORDNUM";
        var expectedOrderGuid = Guid.NewGuid();

        mockOrderNumberGenerator
            .Setup(g => g.GenerateNumber(userGuid))
            .ReturnsAsync(generatedOrderNumber);

        mockOrderRepository
            .Setup(r => r.AddAsync(It.IsAny<Order>()))
            .Callback<Order>(order =>
            {
                order.Guid = expectedOrderGuid;
            });

        var handler = new CreateOrderCommandHandler(mockOrderRepository.Object, mockUnitOfWork.Object, mockOrderNumberGenerator.Object);

        var command = new CreateOrderCommand(userGuid, productList);
        
        //Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        //Assrt
        Assert.Equal(expectedOrderGuid, result);
        
        mockOrderNumberGenerator.Verify(g => g.GenerateNumber(userGuid), Times.Once);
        mockOrderRepository.Verify(r => r.AddAsync(It.Is<Order>(o =>
            o.UserId == userGuid &&
            o.Products.SequenceEqual(productList) &&
            o.Number == generatedOrderNumber)), Times.Once);
        
        mockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
    }
}