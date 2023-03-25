namespace SmarterSheet.Definitions.Models;

public sealed class Report
{
    [JsonPropertyName("sourceSheets")]
    public Sheet[]? Sheets { get; set; }
}