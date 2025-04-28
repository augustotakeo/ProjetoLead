using System.Text.Json.Serialization;

namespace ProjetoLead.API.Dtos;

public class ErrorDetails
{
    [JsonPropertyName("errors")]
    public List<string> Errors { get; set; } = null!;
}
