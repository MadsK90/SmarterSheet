namespace SmarterSheet.Sdk.Requests.Folders;

public sealed class CopyFolderRequest
{
    [JsonPropertyName("destinationType")]
    public string DestinationType { get; set; } = default!;

    [JsonPropertyName("destinationId")]
    public ulong DestinationId { get; set; }

    [JsonPropertyName("newName")]
    public string NewName { get; set; } = default!;
}