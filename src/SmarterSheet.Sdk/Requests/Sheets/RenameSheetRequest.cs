namespace SmarterSheet.Sdk.Requests.Sheets;

internal sealed class RenameSheetRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
}