namespace SmarterSheet.Sdk.Models;

public sealed class ResultObject<T>
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = default!;

    [JsonPropertyName("resultCode")]
    public int ResultCode { get; set; }

    [JsonPropertyName("result")]
    public T Result { get; set; } = default!;
}