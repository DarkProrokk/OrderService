using Application.Commands;
using Application.Handlers;
using Domain.Entity;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Repository;
using Moq;
using Results;
using Tests.Fixture;

namespace Tests.Application.Handlers;

public class CreateOrderCommandHandlerTests: CreateOrderCommandHandlerTestBase
{
    [Fact]
    public async Task Handle_ShouldCreateOrderAndReturnSuccessResultIsSuccessTrue()
    {
        //Arrange
        var userGuid = Guid.NewGuid();
        var productList = new Dictionary<Guid, int> {{Guid.NewGuid(), 5}, { Guid.NewGuid(), 4 }};
        var orderNumber = "2025-0001";
        var expectedOrderGuid = Guid.NewGuid();
        var orderArrange = Order.Create(userGuid, productList.Keys.ToList(), orderNumber);
        var expectedResult = OperationResult.Success();
        
        MockOrderFactory
            .Setup(f => f.CreateAsync(userGuid, productList.Keys.ToList()))
            .ReturnsAsync(orderArrange);

        MockOrderRepository
            .Setup(r => r.AddAsync(It.IsAny<Order>()))
            .Callback<Order>(order =>
            {
                order.Guid = expectedOrderGuid;
            });

        MockMessageBusService
            .Setup(b => b.PublishOrderCreatedForProcessing(It.IsAny<Order>()));

        var handler = new CreateOrderCommandHandler(MockOrderRepository.Object, 
            MockUnitOfWork.Object, 
            MockOrderFactory.Object,
            MockMessageBusService.Object);

        var command = new CreateOrderCommand(userGuid, productList);
        
        //Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        //Assrt
        Assert.Equal(expectedResult.IsSuccess, result.IsSuccess);
        
        MockOrderRepository.Verify(r => r.AddAsync(It.Is<Order>(o =>
            o.UserId == userGuid &&
            o.Products.SequenceEqual(productList.Keys.ToList()) &&
            o.Number == orderNumber)), Times.Once);
        
        MockUnitOfWork.Verify(u => u.SaveAsync(), Times.Once);
        
        MockMessageBusService.Verify(b => b.PublishOrderCreatedForProcessing(It.IsAny<Order>()), Times.Once);
    }
    
    
    [Fact]
    public async Task Handle_WithEmptyProductListShouldReturnOperationResultIsFailure()
    {
        //Arrange
        var userGuid = Guid.NewGuid();
        var productList = new Dictionary<Guid, int>();
        var expectedResult = OperationResult.Failure("The number of products must be greater than 0.");
        
        
        MockOrderFactory
            .Setup(f => f.CreateAsync(userGuid, productList.Keys.ToList()))
            .Throws(() => new OrderCreateArgumentException("The number of products must be greater than 0."));

        MockOrderRepository
            .Setup(r => r.AddAsync(It.IsAny<Order>()));

        MockMessageBusService
            .Setup(b => b.PublishOrderCreatedForProcessing(It.IsAny<Order>()));
        
        
        var handler = new CreateOrderCommandHandler(MockOrderRepository.Object, 
            MockUnitOfWork.Object, 
            MockOrderFactory.Object,
            MockMessageBusService.Object);

        var command = new CreateOrderCommand(userGuid, productList);
        
        //Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        //Assert
        Assert.Equal(expectedResult.Message, result.Message);
        
        MockOrderRepository.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Never);
        
        MockUnitOfWork.Verify(u => u.SaveAsync(), Times.Never);
        
        MockMessageBusService.Verify(b => b.PublishOrderCreatedForProcessing(It.IsAny<Order>()), Times.Never);
    }
    
    [Fact]
    public async Task Handle_WithEmptyUserGuidtShouldReturnException()
    {
        //Arrange
        var userGuid = Guid.Empty;
        var productList = new Dictionary<Guid, int> {{Guid.NewGuid(), 5}, { Guid.NewGuid(), 4 }};
        var expectedResult = OperationResult.Failure("User Guid cannot be default");

        MockOrderFactory
            .Setup(f => f.CreateAsync(userGuid, productList.Keys.ToList()))
            .Throws(() => new OrderCreateArgumentException("User Guid cannot be default"));

        MockOrderRepository
            .Setup(r => r.AddAsync(It.IsAny<Order>()));
        
        MockMessageBusService
            .Setup(b => b.PublishOrderCreatedForProcessing(It.IsAny<Order>()));


        var handler = new CreateOrderCommandHandler(MockOrderRepository.Object, 
            MockUnitOfWork.Object, 
            MockOrderFactory.Object,
            MockMessageBusService.Object);

        var command = new CreateOrderCommand(userGuid, productList);

        //Act and Assert
        var result =  await handler.Handle(command, CancellationToken.None);
        
        Assert.Equal(expectedResult.Message, result.Message);
        
        MockOrderRepository.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Never);
        
        MockUnitOfWork.Verify(u => u.SaveAsync(), Times.Never);
        
        MockMessageBusService.Verify(b => b.PublishOrderCreatedForProcessing(It.IsAny<Order>()), Times.Never);
    }
}