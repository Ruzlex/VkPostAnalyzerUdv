using Domain.Interfaces;
using Domain.Models;

namespace Service.Services;

public class PostAnalyzeService: IPostAnalyzeService
{
    private readonly IPostAnalyzeRepository _postAnalyzeRepository;
    private readonly IVkApiClient _vkApiClient;

    public PostAnalyzeService(IPostAnalyzeRepository postAnalyzeRepository, IVkApiClient vkApiClient)
    {
        _postAnalyzeRepository = postAnalyzeRepository;
        _vkApiClient = vkApiClient;
    }
    
    
    public async Task<List<LetterCount>> AnalyzePostsAsync(string accessToken, long ownerId)
    {
        var response = await _vkApiClient.GetFivePosts(accessToken, ownerId);
        
        var posts = response.Items.Select(x => x.Text);
        var text = string.Join(' ', posts);
        
        var letterCounts = text.ToLower()
            .Where(char.IsLetter)
            .GroupBy(c => c)
            .Select(g => new LetterCount(g.Key, g.Count()))
            .OrderBy(lc => lc.Letter)
            .ToList();

        await _postAnalyzeRepository.ClearAndAddLettersAsync(letterCounts);

        return letterCounts;
    }
}