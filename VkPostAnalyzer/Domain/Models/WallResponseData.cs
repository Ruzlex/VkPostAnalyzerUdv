using System.Text.Json.Serialization;

namespace Domain.Models;

public class WallResponseData
{
    [JsonPropertyName("items")]
    public WallPostText[]? Items { get; set; }
}