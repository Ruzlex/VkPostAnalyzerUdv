using System.Text;
using System.Text.Json;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Options;

namespace Infrastructure.VkSettings;

public class VkApiClient: IVkApiClient
{
    private readonly HttpClient httpClient;
    private readonly string clientId;
    private readonly string redirectUri;
    private readonly string version;

    public VkApiClient(HttpClient httpClient, IOptions<VkApiSettings> settings)
    {
        this.httpClient = httpClient;

        var vkApiSettings = settings.Value;

        clientId = vkApiSettings.ClientId;
        redirectUri = vkApiSettings.RedirectUri;
        version = vkApiSettings.Version;
    }
    
    public string GetUrlForAuth(string state, string codeChallenge)
    {
        return
            $"https://id.vk.com/authorize?response_type=code&client_id={clientId}&redirect_uri={redirectUri}&state={state}&code_challenge={codeChallenge}&code_challenge_method=S256";
    }

    public async Task<string?> GetAccessToken(string code, string deviceId, string codeVerifier)
    {
        var response = await RequestAsync("https://id.vk.com/oauth2/auth",
            $"client_id={clientId}&grant_type=authorization_code&code_verifier={codeVerifier}&device_id={deviceId}&code={code}&redirect_uri={redirectUri}");
        return JsonSerializer.Deserialize<AccessTokenResp>(response)?.AccessToken;
    }

    public async Task<WallResponseData?> GetFivePosts(string accessToken, long ownerId)
    {
        var response = await RequestAsync("https://api.vk.com/method/wall.get",
            $"owner_id={(ownerId == 0 ? "" : ownerId)}&count=5&access_token={accessToken}&v={version}");
        return JsonSerializer.Deserialize<WallResponse>(response)?.Response;
    }
    
    private async Task<string> RequestAsync(string url, string args)
    {
        using var content = new StringContent(args, Encoding.UTF8, "application/x-www-form-urlencoded");
        using var response = await httpClient.PostAsync(url, content);
        return await response.Content.ReadAsStringAsync();
    }
}