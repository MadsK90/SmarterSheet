namespace SmarterSheet.Definitions.Models;

public sealed class Cell
{
    [JsonPropertyName("columnId")]
    public ulong ColumnId { get; set; }

    [JsonPropertyName("value"), JsonConverter(typeof(CellValueConverter))]
    public string? Value { get; set; }

    [JsonPropertyName("displayValue"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DisplayValue { get; set; }

    [JsonPropertyName("formula"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Formula { get; set; }

    [JsonPropertyName("strict"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Strict { get; set; } = true;

    [JsonPropertyName("linkInFromCell"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CellLink? LinkInFromCell { get; set; }
}