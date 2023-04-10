using System.Net.Http.Headers;
using Infrastructure.Models;
using Infrastructure.Models.Requests;
using Infrastructure.Services;

namespace BlazorUI.Services;

public class UserService : IUserService
{

    private readonly HttpClient _httpClient;

    public UserService(HttpClient client)
    {
        _httpClient = client ?? throw new ArgumentNullException(nameof(client));
    }
    public async Task<UserToken> Authenticate(UserAuthenticateRequest userInputData)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/Authentication/Authenticate" , userInputData);
        response.EnsureSuccessStatusCode();

        var userToken = await response.Content.ReadFromJsonAsync<UserToken>();
        
        SetToken(userToken!.Token);

        return userToken!;
    }
    
    
    
    private void SetToken(string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}