namespace SmarterSheet.Definitions.Models;

public sealed class Template
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = default!;

    [JsonPropertyName("accessLevel")]
    public string AccessLevel { get; set; } = default!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
}