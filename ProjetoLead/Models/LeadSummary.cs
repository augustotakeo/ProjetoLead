using System.Text.Json.Serialization;

namespace ProjetoLead.Models;

public class LeadSummary
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("cnpj")]
    public string Cnpj { get; set; } = null!;
    [JsonPropertyName("razaoSocial")]
    public string RazaoSocial { get; set; } = null!;
    [JsonPropertyName("cep")]
    public string CEP { get; set; } = null!;
    [JsonPropertyName("estado")]
    public string Estado { get; set; } = null!;
}