namespace SmarterSheet.Sdk.Requests.Sheets;

internal sealed class CreateSheetFromTemplateRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("fromId")]
    public ulong FromId { get; set; }
}