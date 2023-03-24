namespace SmarterSheet.Sdk.Requests.Rows;

internal class CopyRowsToSheetRequest
{
    [JsonPropertyName("rowIds")]
    public IEnumerable<ulong> RowIds { get; set; } = default!;

    [JsonPropertyName("to")]
    public CopyToSheetObject Sheet { get; set; } = default!;
}