using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AuthRepository: IAuthRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AuthRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(Auth authRequest)
    {
        await _dbContext.Auth.AddAsync(authRequest);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Auth?> GetByStateAsync(string state)
    {
        return await _dbContext.Auth.FirstOrDefaultAsync(x => x!.State == state);
    }

    public async Task RemoveAsync(Auth authRequest)
    {
        _dbContext.Auth.Remove(authRequest);
        await _dbContext.SaveChangesAsync();
    }
}