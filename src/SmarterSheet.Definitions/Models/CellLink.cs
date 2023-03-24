namespace SmarterSheet.Definitions.Models;

public sealed class CellLink
{
    [JsonPropertyName("columnId")]
    public ulong ColumnId { get; set; }

    [JsonPropertyName("sheetId")]
    public ulong SheetId { get; set; }

    [JsonPropertyName("rowId")]
    public ulong RowId { get; set; }
}