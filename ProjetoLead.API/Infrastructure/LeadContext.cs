using Microsoft.EntityFrameworkCore;
using ProjetoLead.API.Infrastructure.EntityConfigurations;
using ProjetoLead.API.Models;

namespace ProjetoLead.API.Infrastructure;

public class LeadContext : DbContext
{

    public DbSet<Lead> Leads { get; set; }

    public LeadContext(DbContextOptions<LeadContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LeadEntityTypeConfigurations());
        base.OnModelCreating(modelBuilder);
    }
}