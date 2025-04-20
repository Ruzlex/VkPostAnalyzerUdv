using Domain.Models;

namespace Domain.Interfaces;

public interface IPostAnalyzeService
{
    Task<List<LetterCount>> AnalyzePostsAsync(string accessToken, long ownerId);
}