namespace SmarterSheet.Sdk.Requests.Folders;

internal sealed class RenameFolderRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}