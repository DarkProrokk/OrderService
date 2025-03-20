using Domain.Entity;
using Domain.Interfaces;
using Domain.Repository;
using Moq;

namespace Tests.Fixture;

public class CreateOrderCommandHandlerTestBase
{
    protected readonly Mock<IOrderFactory> MockOrderFactory;
    protected readonly Mock<IOrderRepository> MockOrderRepository;
    protected readonly Mock<IUnitOfWork> MockUnitOfWork;
    

    protected CreateOrderCommandHandlerTestBase()
    {
        MockOrderRepository = new Mock<IOrderRepository>();
        MockUnitOfWork = new Mock<IUnitOfWork>();
        MockOrderFactory = new Mock<IOrderFactory>();

    }
}