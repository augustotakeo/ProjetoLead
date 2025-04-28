using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ProjetoLead;
using ProjetoLead.Services;
using ProjetoLead.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddHttpClient(HttpClientIdentifiers.ViaCepAPI, client =>
{
    client.BaseAddress = new Uri("https://viacep.com.br");
});
builder.Services.AddHttpClient(HttpClientIdentifiers.ProjetoLeadAPI, client =>
{
    client.BaseAddress = new Uri("http://localhost:5148");
});

builder.Services.AddScoped<LeadService>();

await builder.Build().RunAsync();
