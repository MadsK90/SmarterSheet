namespace SmarterSheet.Sdk.Requests.Sheets;

internal sealed class MoveSheetRequest
{
    [JsonPropertyName("destinationType")]
    public string DestinationType { get; set; } = default!;

    [JsonPropertyName("destinationId")]
    public ulong DestinationId { get; set; }
}