using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace VkPostAnalyzer.Controllers;
[ApiController]
[Route("api/auth")]
public class AuthController: ControllerBase
{
    private readonly IAuthService _AuthService;

    public AuthController(IAuthService AuthService)
    {
        _AuthService = AuthService;
    }
    
    [HttpGet("url")]
    public async Task<IActionResult> GetAuthUrl()
    {
        var result = await _AuthService.GenerateUrlForAuth();
        return Ok(new {url = result});
    }
    
    [HttpGet("response")]
    public async Task<IActionResult> HandleAuthResponse([FromQuery] string code, [FromQuery(Name = "device_id")] string deviceId, [FromQuery] string state)
    {
        var result = await _AuthService.AuthResponseProcessing(code, deviceId, state);
        return Ok(new{access_token = result});
    }
}