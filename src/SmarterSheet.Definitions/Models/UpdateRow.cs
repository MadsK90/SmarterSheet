namespace SmarterSheet.Definitions.Models;

public sealed class UpdateRow
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("parentId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ulong ParentId { get; set; }

    [JsonPropertyName("toBottom"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool ToBottom { get; set; }

    [JsonPropertyName("toTop"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool ToTop { get; set; }

    [JsonPropertyName("expanded"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Expanded { get; set; }

    [JsonPropertyName("cells")]
    public Cell[]? Cells { get; set; }
}