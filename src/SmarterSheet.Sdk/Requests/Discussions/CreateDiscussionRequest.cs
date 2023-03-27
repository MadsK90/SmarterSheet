namespace SmarterSheet.Sdk.Requests.Discussions;

internal sealed class CreateDiscussionRequest
{
    [JsonPropertyName("commment")]
    public Comment Comment { get; set; } = default!;
}