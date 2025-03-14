using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repository;

public class UnitOfWork(OrderContext context): IUnitOfWork
{
    public async Task SaveAsync() => await context.SaveChangesAsync();
}