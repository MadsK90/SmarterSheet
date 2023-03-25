namespace SmarterSheet.Definitions.Models;

public sealed class Folder
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("favorite")]
    public bool Favourite { get; set; }

    [JsonPropertyName("folders")]
    public Folder[]? SubFolders { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("permalink")]
    public string Permalink { get; set; } = default!;

    [JsonPropertyName("reports")]
    public Report[]? Reports { get; set; }

    [JsonPropertyName("sheets")]
    public Sheet[]? Sheets { get; set; }

    [JsonPropertyName("templates")]
    public Template[]? Templates { get; set; }
}