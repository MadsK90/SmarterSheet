namespace SmarterSheet.Sdk.Requests.Sheet;

internal sealed class MoveSheetRequest
{
    [JsonPropertyName("destinationType")]
    public string DestinationType { get; set; } = default!;

    [JsonPropertyName("destinationId")]
    public ulong DestinationId { get; set; }
}