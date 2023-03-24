namespace SmarterSheet.Definitions.Models;

public sealed class Sheet
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("fromId")]
    public ulong FromId { get; set; }

    [JsonPropertyName("ownerId")]
    public ulong OwnerId { get; set; }

    [JsonPropertyName("accessLevel")]
    public string AccessLevel { get; set; } = default!;

    [JsonPropertyName("columns")]
    public Column[]? Columns { get; set; }

    [JsonPropertyName("discussions")]
    public Discussion[]? Discussions { get; set; }

    [JsonPropertyName("favorite")]
    public bool Favourite { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("owner")]
    public string Owner { get; set; } = default!;

    [JsonPropertyName("permalink")]
    public string Permalink { get; set; } = default!;

    [JsonPropertyName("totalRowCount")]
    public int TotalRowCount { get; set; }

    [JsonPropertyName("rows")]
    public Row[]? Rows { get; set; }

    [JsonPropertyName("modifiedAt")]
    public DateTime ModifiedAt { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("readOnly")]
    public bool ReadOnly { get; set; }
}