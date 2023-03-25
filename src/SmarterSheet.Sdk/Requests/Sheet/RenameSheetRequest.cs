namespace SmarterSheet.Sdk.Requests.Sheet;

internal sealed class RenameSheetRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;
}