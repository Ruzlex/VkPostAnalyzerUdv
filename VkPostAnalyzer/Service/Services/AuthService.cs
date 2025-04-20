using Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;
using Domain.Models;
using Microsoft.AspNetCore.Authentication;

namespace Service.Services;

public class AuthService: IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IVkApiClient _vkApiClient;

    public AuthService(IAuthRepository authRepository, IVkApiClient vkApiClient)
    {
        _authRepository = authRepository;
        _vkApiClient = vkApiClient;
    }
    
    public async Task<string> GenerateUrlForAuth()
    {
        var codeVerifier = GenerateCodeVerifier();
        var codeChallenge = GetCodeChallenge(codeVerifier);
        var state = GenerateState();
        var authUrl = _vkApiClient.GetUrlForAuth(state, codeChallenge);

        var auth = new Auth(state, codeVerifier);
        await _authRepository.AddAsync(auth);

        return authUrl;
    }

    public async Task<string?> AuthResponseProcessing(string code, string deviceId, string state)
    {
        var auth = await _authRepository.GetByStateAsync(state);
        await _authRepository.RemoveAsync(auth);
        var accessToken = await _vkApiClient.GetAccessToken(code, deviceId, auth.CodeVerifier);
        return accessToken;
    }
    
    private static string GenerateCodeVerifier() => Base64UrlTextEncoder.Encode(RandomNumberGenerator.GetBytes(16));
    private static string GenerateState() => Base64UrlTextEncoder.Encode(RandomNumberGenerator.GetBytes(8));
    private static string GetCodeChallenge(string codeVerifier) =>
        Base64UrlTextEncoder.Encode(SHA256.HashData(Encoding.UTF8.GetBytes(codeVerifier)));
}