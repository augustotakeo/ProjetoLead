namespace ProjetoLead.API.Models;

public class Lead
{
    public int Id { get; set; }
    public string Cnpj { get; set; } = null!;
    public string RazaoSocial { get; set; } = null!;
    public string CEP { get; set; } = null!;
    public string Endereco { get; set; } = null!;
    public string Numero { get; set; } = null!;
    public string? Complemento { get; set; } = null!;
    public string Bairro { get; set; } = null!;
    public string Cidade { get; set; } = null!;
    public string Estado { get; set; } = null!;
}