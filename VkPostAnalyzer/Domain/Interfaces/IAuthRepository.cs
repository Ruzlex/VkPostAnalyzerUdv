using Domain.Models;

namespace Domain.Interfaces;

public interface IAuthRepository
{
    Task AddAsync(Auth authRequest);
    Task<Auth?> GetByStateAsync(string state);
    Task RemoveAsync(Auth authRequest);
}