namespace SmarterSheet.Definitions.Models;

public sealed class Workspace
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("accessLevel")]
    public string AccessLevel { get; set; } = default!;
    
    [JsonPropertyName("favourite")]
    public bool Favorite { get; set; }

    [JsonPropertyName("folders")]
    public Folder[]? Folders { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("permaLin")]
    public string Permalink { get; set; } = default!;

    [JsonPropertyName("reports")]
    public Report[]? Reports { get; set; }

    [JsonPropertyName("sheets")]
    public Sheet[]? Sheets { get; set; }

    [JsonPropertyName("templates")]
    public Template[]? Templates { get; set; }
}