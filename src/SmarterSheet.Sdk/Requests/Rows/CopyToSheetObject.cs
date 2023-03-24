namespace SmarterSheet.Sdk.Requests.Rows;

internal class CopyToSheetObject
{
    [JsonPropertyName("sheetId")]
    public ulong SheetId { get; set; }
}