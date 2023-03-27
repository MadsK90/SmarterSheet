namespace SmarterSheet.Sdk.Requests.Folders;

internal sealed class CreateFolderRequest
{
    [JsonPropertyName("name")]
    public string FolderName { get; set; } = default!;
}