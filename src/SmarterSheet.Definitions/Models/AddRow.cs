namespace SmarterSheet.Definitions.Models;

public sealed class AddRow
{
    [JsonPropertyName("parentId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ulong ParentId { get; set; }

    [JsonPropertyName("siblingId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ulong SiblingId { get; set; }

    [JsonPropertyName("toBottom"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool ToBottom { get; set; }

    [JsonPropertyName("toTop"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool ToTop { get; set; }

    [JsonPropertyName("above"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Above { get; set; }

    [JsonPropertyName("indent"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? Indent { get; set; }

    [JsonPropertyName("outdent"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Outdent { get; set; }

    [JsonPropertyName("expanded"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Expanded { get; set; }

    [JsonPropertyName("cells")]
    public Cell[]? Cells { get; set; }
}