using ProjetoLead.API.Dtos;

namespace ProjetoLead.API.Infrastructure.Repositories;

public interface ILeadRepository
{
    Task<List<LeadSummaryDto>> GetAllAsync();
    Task<LeadDetailDto?> GetAsync(int id);
    Task<LeadSummaryDto> CreateAsync(LeadDetailDto leadDetail);
    Task<LeadSummaryDto?> UpdateAsync(LeadDetailDto leadDetail);
    Task<bool> DeleteAsync(int id);
}
