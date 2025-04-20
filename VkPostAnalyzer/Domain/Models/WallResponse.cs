using System.Text.Json.Serialization;

namespace Domain.Models;

public class WallResponse
{
    [JsonPropertyName("response")]
    public WallResponseData? Response { get; set; }
}