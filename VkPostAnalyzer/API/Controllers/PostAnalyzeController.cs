using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using VkPostAnalyzer.Logs;

namespace VkPostAnalyzer.Controllers;

[ApiController]
[Route("api/analyze_post")]
public class PostAnalyzeController: ControllerBase
{
    private readonly IPostAnalyzeService _postAnalyzeService;
    private readonly ActionLogger _actionLogger;

    public PostAnalyzeController(IPostAnalyzeService postAnalyzeService, ActionLogger actionLogger)
    {
        _postAnalyzeService = postAnalyzeService;
        _actionLogger = actionLogger;
    }

    [HttpPost("analyzing")]
    public async Task<IActionResult> AnalyzePosts([FromBody] AnalyzeRequest analyzeRequest)
    {
        _actionLogger.LogStart(analyzeRequest.OwnerId);
        var result = await _postAnalyzeService.AnalyzePostsAsync(analyzeRequest.AccessToken, analyzeRequest.OwnerId);
        _actionLogger.LogEnd(analyzeRequest.OwnerId);
        return Ok(new { res = result });
    }
    
    public class AnalyzeRequest
    {
        public string AccessToken { get; set; }
        public long OwnerId { get; set; }
    }
}