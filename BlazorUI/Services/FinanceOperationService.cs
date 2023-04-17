using BlazorUI.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;
using Infrastructure.Services;
using Newtonsoft.Json;

namespace BlazorUI.Services;

public class FinanceOperationService : IFinanceOperationService
{
    private readonly IHttpClientHandler _httpClient;
    
    public FinanceOperationService(IHttpClientHandler httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
    
    public async Task<IEnumerable<FinanceOperation>> GetFinanceOperations()
    {
        return (await _httpClient.GetFromJsonAsync<FinanceOperation[]>("api/FinanceOperations/GetAllFinanceOperations"))!;
    }

    public async Task<FinanceOperation> GetFinanceOperation(int id)
    {
        var response = await _httpClient.GetAsync($"api/FinanceOperations/GetFinanceOperation?id={id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<FinanceOperation>();
    }

    public async Task<FinanceOperationAddResponse> CreateFinanceOperation(FinanceOperationAddRequest type)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/FinanceOperations/CreateFinanceOperation", type);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<FinanceOperationAddResponse>();
        
    }

    public async Task RemoveFinanceOperation(int id)
    {
        var response =  await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, $"api/FinanceOperations/RemoveFinanceOperation?id={id}"));
        response.EnsureSuccessStatusCode();
    }

    public async Task<FinanceOperationUpdateResponse> UpdateFinanceOperation(FinanceOperationUpdateRequest type)
    {
        var response  = await _httpClient.PutAsJsonAsync("api/FinanceOperations/UpdateFinanceOperation" , type);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<FinanceOperationUpdateResponse>(content)!;
    }
}