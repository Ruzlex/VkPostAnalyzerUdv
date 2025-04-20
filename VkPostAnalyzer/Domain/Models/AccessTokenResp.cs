using System.Text.Json.Serialization;

namespace Domain.Models;

public class AccessTokenResp
{
    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }
}