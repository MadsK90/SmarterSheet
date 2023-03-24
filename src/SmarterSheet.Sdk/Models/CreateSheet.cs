namespace SmarterSheet.Sdk.Models;

internal class CreateSheet
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("columns")]
    public CreateColumn[] Columns { get; set; } = default!;
}