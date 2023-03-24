namespace SmarterSheet.Definitions.Models;

public sealed class Attachment
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("parentId")]
    public ulong ParentId { get; set; }

    [JsonPropertyName("attachmentType")]
    public string AttachmentType { get; set; } = default!;

    [JsonPropertyName("attachmentSubType")]
    public string AttachmentSubType { get; set; } = default!;

    [JsonPropertyName("mimeType")]
    public string MimeType { get; set; } = default!;

    [JsonPropertyName("parentType")]
    public string ParentType { get; set; } = default!;

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("created")]
    public User CreatedBy { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("sizeInKb")]
    public int SizeInKb { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; } = default!;

    [JsonPropertyName("urlExpiresInMillis")]
    public ulong UrlExpiresIn { get; set; }
}