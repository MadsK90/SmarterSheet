namespace SmarterSheet.Definitions.Models;

public sealed class EmptyResultObject
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    [JsonPropertyName("resultCode")]
    public int ResultCode { get; set; }
}