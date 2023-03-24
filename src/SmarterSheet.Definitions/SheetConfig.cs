namespace SmarterSheet.Definitions;

public sealed class SheetConfig
{
    public bool DebugLog { get; set; }
    public string ApiKey { get; init; } = null!;
    public string BasePath { get; init; } = null!;
}