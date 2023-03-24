namespace SmarterSheet.Sdk.Models;

internal class CreateColumn
{
    [JsonPropertyName("primary")]
    public bool Primary { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; } = default!;

    [JsonPropertyName("type")]
    public ColumnType Type { get; set; }
}