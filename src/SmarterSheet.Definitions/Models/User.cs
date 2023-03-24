namespace SmarterSheet.Definitions.Models;

public sealed class User
{
    [JsonPropertyName("id")]
    public ulong Id { get; set; }

    [JsonPropertyName("admin")]
    public bool Admin { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; } = default!;

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = default!;

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = default!;

    [JsonPropertyName("groupAdmin")]
    public bool GroupAdmin { get; set; }

    [JsonPropertyName("lastLogin")]
    public ulong LastLogin { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("status")]
    public string Status { get; set; } = default!;
}