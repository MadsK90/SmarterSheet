namespace SmarterSheet.Definitions.Models;

public sealed class Column
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("virtualId")]
    public ulong VirtualId { get; set; }

    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("index")]
    public int Index { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = default!;

    [JsonPropertyName("type")]
    public ColumnType Type { get; set; }

    [JsonPropertyName("primary")]
    public bool Primary { get; set; }

    [JsonPropertyName("validation")]
    public bool Validation { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }
}