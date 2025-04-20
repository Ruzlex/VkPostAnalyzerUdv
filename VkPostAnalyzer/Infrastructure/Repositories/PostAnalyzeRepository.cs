using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PostAnalyzeRepository: IPostAnalyzeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PostAnalyzeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task ClearAndAddLettersAsync(IEnumerable<LetterCount> letterCounts)
    {
        await _dbContext.LetterCounts.ExecuteDeleteAsync();
        await _dbContext.LetterCounts.AddRangeAsync(letterCounts);
        await _dbContext.SaveChangesAsync();
    }
}