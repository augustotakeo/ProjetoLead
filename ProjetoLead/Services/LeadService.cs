using System.Text;
using System.Text.Json;
using MudBlazor;
using ProjetoLead.API.Dtos;
using ProjetoLead.Models;
using ProjetoLead.Shared;

namespace ProjetoLead.Services;

public class LeadService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IDialogService _dialogService;
    private readonly ISnackbar _snackbar;
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = false
    };

    public LeadService(IHttpClientFactory httpClientFactory, IDialogService dialogService, ISnackbar snackbar)
    {
        _httpClientFactory = httpClientFactory;
        _dialogService = dialogService;
        _snackbar = snackbar;
    }

    public async Task<List<LeadSummary>?> GetLeadsAsync()
    {
        var response = await _httpClientFactory
            .CreateClient(HttpClientIdentifiers.ProjetoLeadAPI)
            .GetAsync("api/lead");

        if (!response.IsSuccessStatusCode)
        {
            await _dialogService.ShowMessageBox("Busca dos Leads", "Não foi possível buscar os leads");
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<LeadSummary>>(content, _options);
    }

    public async Task<LeadDetail?> GetLeadAsync(int id)
    {
        var response = await _httpClientFactory
                    .CreateClient(HttpClientIdentifiers.ProjetoLeadAPI)
                    .GetAsync($"api/lead/{id}");

        if (!response.IsSuccessStatusCode)
        {
            await _dialogService.ShowMessageBox("Busca do Lead", "Não foi possível encontrar o lead");
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<LeadDetail>(content, _options);
    }

    public async Task<PostalCodeDetail?> GetPostalCodeDetail(string cep)
    {
        var response = await _httpClientFactory
            .CreateClient(HttpClientIdentifiers.ViaCepAPI)
            .GetAsync($"ws/{cep}/json");

        if (!response.IsSuccessStatusCode)
        {
            await _dialogService.ShowMessageBox("Busca CEP", "Não foi possível encontrar o CEP");
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PostalCodeDetail>(content);
    }

    public async Task<bool> CreateLeadAsync(LeadDetail lead)
    {
        using StringContent jsonContent = ConvertToStringContent(lead);

        var response = await _httpClientFactory
            .CreateClient(HttpClientIdentifiers.ProjetoLeadAPI)
            .PostAsync("api/lead", jsonContent);

        var content = await response.Content.ReadAsStreamAsync();

        if (!response.IsSuccessStatusCode)
        {
            var errorDetails = JsonSerializer.Deserialize<ErrorDetails>(content);
            await _dialogService.ShowMessageBox("Cadastro de Lead", string.Join(";  ", errorDetails?.Errors ?? []));
        }

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateLeadAsync(LeadDetail lead)
    {
        using StringContent jsonContent = ConvertToStringContent(lead);

        var response = await _httpClientFactory
            .CreateClient(HttpClientIdentifiers.ProjetoLeadAPI)
            .PutAsync("api/lead", jsonContent);

        var content = await response.Content.ReadAsStreamAsync();

        if (!response.IsSuccessStatusCode)
        {
            var errorDetails = JsonSerializer.Deserialize<ErrorDetails>(content);
            await _dialogService.ShowMessageBox("Atualização de Lead", string.Join(";  ", errorDetails?.Errors ?? []));
        }

        return response.IsSuccessStatusCode;
    }

    private StringContent ConvertToStringContent(object obj)
    {
        return new(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
    }

    public async Task<bool> DeleteLeadAsync(int id)
    {
        var response = await _httpClientFactory
            .CreateClient(HttpClientIdentifiers.ProjetoLeadAPI)
            .DeleteAsync($"api/lead/{id}");

        if (!response.IsSuccessStatusCode)
            _snackbar.Add("Não foi possível excluir o lead");

        return response.IsSuccessStatusCode;
    }
}