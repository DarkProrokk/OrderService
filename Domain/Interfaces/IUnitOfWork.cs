namespace Domain.Interfaces;

public interface IUnitOfWork
{
    public Task SaveAsync();
}