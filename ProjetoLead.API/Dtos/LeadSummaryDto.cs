namespace ProjetoLead.API.Dtos;

public class LeadSummaryDto
{
    public int Id { get; set; }
    public string Cnpj { get; set; } = null!;
    public string RazaoSocial { get; set; } = null!;
    public string CEP { get; set; } = null!;
    public string Estado { get; set; } = null!;

    public LeadSummaryDto() { }

    public LeadSummaryDto(LeadDetailDto leadDetail)
    {
        Id = leadDetail.Id;
        Cnpj = leadDetail.Cnpj;
        RazaoSocial = leadDetail.RazaoSocial;
        CEP = leadDetail.CEP;
        Estado = leadDetail.Estado;
    }
}