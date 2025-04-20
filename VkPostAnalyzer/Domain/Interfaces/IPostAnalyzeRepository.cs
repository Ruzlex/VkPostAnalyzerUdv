using Domain.Models;

namespace Domain.Interfaces;

public interface IPostAnalyzeRepository
{
    Task ClearAndAddLettersAsync(IEnumerable<LetterCount> letterCounts);
}