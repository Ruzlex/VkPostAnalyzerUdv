using Domain.Models;

namespace Domain.Interfaces;

public interface IVkApiClient
{
    string GetUrlForAuth(string state, string codeChallenge);
    Task<string?> GetAccessToken(string code, string deviceId, string codeVerifier);
    Task<WallResponseData?> GetFivePosts(string accessToken, long ownerId);
}