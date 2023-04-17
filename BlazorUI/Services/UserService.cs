using System.Text.Json;
using BlazorUI.Services.Interfaces;
using Infrastructure.Models;
using Infrastructure.Models.Requests;
using Infrastructure.Services;
using Newtonsoft.Json;

namespace BlazorUI.Services;

public class UserService : IUserService
{

    private readonly IHttpClientHandler _httpClient;

    public UserService(IHttpClientHandler client)
    {
        _httpClient = client ?? throw new ArgumentNullException(nameof(client));
    }
    public async Task<UserToken> Authenticate(UserAuthenticateRequest userInputData)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/Authentication/Authenticate" , userInputData);
        response.EnsureSuccessStatusCode();
        
        await using var stream = await response.Content.ReadAsStreamAsync();
        var jsonDoc = await JsonDocument.ParseAsync(stream);
        var resultProperty = jsonDoc.RootElement.GetProperty("result");
        var userToken = JsonConvert.DeserializeObject<UserToken>(resultProperty.GetRawText());

        return userToken!;
    }
    
}