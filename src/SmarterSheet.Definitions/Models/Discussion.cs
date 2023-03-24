namespace SmarterSheet.Definitions.Models;

public sealed class Discussion
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("parentId")]
    public ulong ParentId { get; set; }

    [JsonPropertyName("parentType")]
    public string ParentType { get; set; } = default!;

    [JsonPropertyName("accessLevel")]
    public string AccessLevel { get; set; } = default!;

    [JsonPropertyName("commentAttachments")]
    public Attachment[]? CommentAttachments { get; set; }

    [JsonPropertyName("commentCount")]
    public int CommentCount { get; set; }

    [JsonPropertyName("comments")]
    public Comment[]? Comments { get; set; }

    [JsonPropertyName("createdBy")]
    public User CreatedBy { get; set; } = default!;

    [JsonPropertyName("lastCommentedAt")]
    public string LastCommentedAt { get; set; } = default!;

    [JsonPropertyName("lastCommentedUser")]
    public User LastCommentedUser { get; set; } = default!;

    [JsonPropertyName("readOnly")]
    public bool ReadOnly { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = default!;
}