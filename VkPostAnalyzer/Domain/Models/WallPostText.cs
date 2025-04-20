using System.Text.Json.Serialization;

namespace Domain.Models;

public class WallPostText
{
    [JsonPropertyName("text")]
    public string? Text { get; set; }
}