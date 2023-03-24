namespace SmarterSheet.Definitions.Models;

public sealed class Row
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("attachments")]
    public Attachment[]? Attachments { get; set; }

    [JsonPropertyName("discussions")]
    public Discussion[]? Discussions { get; set; }

    [JsonPropertyName("parentId")]
    public ulong ParentId { get; set; }

    [JsonPropertyName("rowNumber")]
    public int RowNumber { get; set; }

    [JsonPropertyName("expanded")]
    public bool Expanded { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("modifiedAt")]
    public DateTime ModifiedAt { get; set; }

    [JsonPropertyName("cells")]
    public Cell[]? Cells { get; set; }

    [JsonPropertyName("sheetId")]
    public ulong SheetId { get; set; }
}