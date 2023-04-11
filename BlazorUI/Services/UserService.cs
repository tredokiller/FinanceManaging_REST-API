using System.Text.Json;
using Infrastructure.Models;
using Infrastructure.Models.Requests;
using Infrastructure.Services;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

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

        //Console.WriteLine(response.Content.ReadAsStringAsync());
        //UserToken userToken = await response.Content.ReadFromJsonAsync<UserToken>();

        await using var stream = await response.Content.ReadAsStreamAsync();
        var jsonDoc = await JsonDocument.ParseAsync(stream);
        var resultProperty = jsonDoc.RootElement.GetProperty("result");
        var userToken = JsonConvert.DeserializeObject<UserToken>(resultProperty.GetRawText());

        return userToken!;
    }
    
}