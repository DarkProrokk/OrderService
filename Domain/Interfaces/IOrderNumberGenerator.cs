namespace Domain.Interfaces;

public interface IOrderNumberGenerator
{
    Task<string> GenerateNumber(Guid userId);
}