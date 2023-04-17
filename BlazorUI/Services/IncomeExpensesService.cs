using System.Net;
using BlazorUI.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;
using Infrastructure.Services;
using Newtonsoft.Json;

namespace BlazorUI.Services;

public class IncomeExpensesService : IIncomeExpenseService
{
    private readonly IHttpClientHandler _httpClient;
    
    public IncomeExpensesService(IHttpClientHandler httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
    public async  Task<IEnumerable<IncomeExpenseCategory>> GetIncomeExpenseTypes()
    {
        return await _httpClient.GetFromJsonAsync<IncomeExpenseCategory[]>("api/IncomeExpenses/GetAllIncomeExpenses");
    }

    public async Task<IncomeExpenseCategory> GetIncomeExpenseType(int id)
    {
        var response = await _httpClient.GetAsync($"api/IncomeExpenses/GetIncomeExpense?id={id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IncomeExpenseCategory>();
        
            
    }

    public async  Task<IncomeExpensesAddResponse> CreateIncomeExpenseType(IncomeExpensesAddRequest type)
    {
        var response = await _httpClient.PostAsJsonAsync("api/IncomeExpenses/CreateIncomeExpenseType" , type);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<IncomeExpensesAddResponse>();
    }

    public async Task RemoveIncomeExpenseType(int id)
    {
        var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, $"api/IncomeExpenses/RemoveIncomeExpenseType?id={id}"));
        
        response.EnsureSuccessStatusCode();
    }

    public async Task<IncomeExpensesUpdateResponse> UpdateIncomeExpenseType(IncomeExpensesUpdateRequest type)
    {
        var response  = await _httpClient.PutAsJsonAsync("api/IncomeExpenses/UpdateIncomeExpenseType" , type);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<IncomeExpensesUpdateResponse>(content)!;
    }
}