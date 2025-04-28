using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoLead.API.Models;

namespace ProjetoLead.API.Infrastructure.EntityConfigurations;

public class LeadEntityTypeConfigurations : IEntityTypeConfiguration<Lead>
{
    public void Configure(EntityTypeBuilder<Lead> builder)
    {
        builder.ToTable("CadastroLead");
        builder.Property(x => x.Cnpj).HasMaxLength(14).IsRequired(true);
        builder.Property(x => x.RazaoSocial).HasMaxLength(60).IsRequired(true);
        builder.Property(x => x.Endereco).HasMaxLength(500).IsRequired(true);
        builder.Property(x => x.Numero).HasMaxLength(50).IsRequired(true);
        builder.Property(x => x.Complemento).HasMaxLength(500).IsRequired(false);
        builder.Property(x => x.Bairro).HasMaxLength(50).IsRequired(true);
        builder.Property(x => x.Cidade).HasMaxLength(50).IsRequired(true);
        builder.Property(x => x.Estado).HasMaxLength(50).IsRequired(true);
    }
}