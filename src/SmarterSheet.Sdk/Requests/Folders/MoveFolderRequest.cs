namespace SmarterSheet.Sdk.Requests.Folders;

internal sealed class MoveFolderRequest
{
    [JsonPropertyName("destinationType")]
    public string DestinationType { get; set; }

    [JsonPropertyName("destinationId")]
    public ulong DestinationId { get; set; }
}