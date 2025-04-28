using System.Text.Json.Serialization;

namespace ProjetoLead.Models;

public class LeadDetail
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("cnpj")]
    public string Cnpj { get; set; } = null!;
    [JsonPropertyName("razaoSocial")]
    public string RazaoSocial { get; set; } = null!;
    [JsonPropertyName("cep")]
    public string CEP { get; set; } = null!;
    [JsonPropertyName("endereco")]
    public string Endereco { get; set; } = null!;
    [JsonPropertyName("numero")]
    public string Numero { get; set; } = null!;
    [JsonPropertyName("complemento")]
    public string? Complemento { get; set; } = null!;
    [JsonPropertyName("bairro")]
    public string Bairro { get; set; } = null!;
    [JsonPropertyName("cidade")]
    public string Cidade { get; set; } = null!;
    [JsonPropertyName("estado")]
    public string Estado { get; set; } = null!;
}
