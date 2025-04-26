using Microsoft.EntityFrameworkCore;
using ProjetoLead.API.Dtos;
using ProjetoLead.API.Models;

namespace ProjetoLead.API.Infrastructure.Repositories;

public class LeadRepository : ILeadRepository
{
    private readonly LeadContext _context;
    public LeadRepository(LeadContext context)
    {
        _context = context;
    }

    public async Task<List<LeadSummaryDto>> GetAllAsync()
    {
        var leads = await
            _context.Leads
                .Select(x => new LeadSummaryDto
                {
                    Id = x.Id,
                    CEP = x.CEP,
                    Cnpj = x.Cnpj,
                    Estado = x.Estado
                })
                .ToListAsync();

        return leads;
    }

    public async Task<LeadDetailDto?> GetAsync(int id)
    {
        var lead = await
            _context.Leads
                .Where(x => x.Id == id)
                .Select(x => new LeadDetailDto
                {
                    Id = x.Id,
                    Bairro = x.Bairro,
                    CEP = x.CEP,
                    Cidade = x.Cidade,
                    Cnpj = x.Cnpj,
                    Complemento = x.Complemento,
                    Endereco = x.Endereco,
                    Estado = x.Estado,
                    Numero = x.Numero,
                    RazaoSocial = x.RazaoSocial
                })
                .FirstOrDefaultAsync();

        return lead;
    }

    public async Task<LeadSummaryDto> CreateAsync(LeadDetailDto leadDetail)
    {
        var lead = new Lead
        {
            Bairro = leadDetail.Bairro,
            CEP = leadDetail.CEP,
            Cidade = leadDetail.Cidade,
            Cnpj = leadDetail.Cnpj,
            Complemento = leadDetail.Complemento,
            Endereco = leadDetail.Endereco,
            Estado = leadDetail.Estado,
            Numero = leadDetail.Numero,
            RazaoSocial = leadDetail.RazaoSocial
        };

        _context.Leads.Add(lead);
        await _context.SaveChangesAsync();

        return new LeadSummaryDto(leadDetail);
    }

    public async Task<LeadSummaryDto?> UpdateAsync(LeadDetailDto leadDetail)
    {
        var n = await
            _context.Leads
                .Where(x => x.Id == leadDetail.Id)
                .ExecuteUpdateAsync(setPropertyCalls => setPropertyCalls
                    .SetProperty(x => x.Bairro, leadDetail.Bairro)
                    .SetProperty(x => x.CEP, leadDetail.CEP)
                    .SetProperty(x => x.Cidade, leadDetail.Cidade)
                    .SetProperty(x => x.Cnpj, leadDetail.Cnpj)
                    .SetProperty(x => x.Complemento, leadDetail.Complemento)
                    .SetProperty(x => x.Endereco, leadDetail.Endereco)
                    .SetProperty(x => x.Estado, leadDetail.Estado)
                    .SetProperty(x => x.Numero, leadDetail.Numero)
                    .SetProperty(x => x.RazaoSocial, leadDetail.RazaoSocial));

        if (n == 0)
            return null;

        return new LeadSummaryDto(leadDetail);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var n = await
            _context.Leads
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();

        return n > 0;
    }

}