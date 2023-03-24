namespace SmarterSheet.Definitions.Models;

public sealed class Comment
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("discussionId")]
    public ulong DiscussionId { get; set; }

    [JsonPropertyName("attachments")]
    public Attachment[]? Attachments { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("createdBy")]
    public User CreatedBy { get; set; } = default!;

    [JsonPropertyName("modifiedAt")]
    public DateTime ModifiedAt { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; } = default!;
}