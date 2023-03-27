namespace SmarterSheet.Sdk.Requests.Workspaces;

internal sealed class CreateWorkspaceRequest
{
    [JsonPropertyName("name")]
    public string WorkspaceName { get; set; } = default!;
}