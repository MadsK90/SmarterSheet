namespace SmarterSheet.Sdk.Requests.Workspaces;

internal sealed class CopyWorkspaceRequest
{
    [JsonPropertyName("newName")]
    public string Name { get; set; } = default!;
}