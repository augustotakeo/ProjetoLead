using Microsoft.EntityFrameworkCore;
using ProjetoLead.API.Infrastructure;
using ProjetoLead.API.Infrastructure.Repositories;

namespace ProjetoLead.API.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<LeadContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("ProjetoLead")));

        serviceCollection.AddScoped<ILeadRepository, LeadRepository>();
    }
}