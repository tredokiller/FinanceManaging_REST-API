using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using BlazorUI.Services.Interfaces;
using Infrastructure.Models.Exceptions;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorUI.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly IHttpClientHandler _httpClient;
    
    public CustomAuthStateProvider(ILocalStorageService localStorageService, IHttpClientHandler httpClient)
    {
        _localStorageService = localStorageService ?? throw new ArgumentNullException(nameof(localStorageService));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
    
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string token = await  _localStorageService.GetItemAsStringAsync("token");

        var identity = new ClaimsIdentity();
        _httpClient.SetAuthorization(null);
        
        if (!string.IsNullOrEmpty(token))
        {
            identity = new ClaimsIdentity(ParseClaimsFromJwt(token) , "jwt");
            _httpClient.SetAuthorization(new AuthenticationHeaderValue("Bearer", token.Replace("\"", "")));

        }
        
        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);
        
        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }



    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payLoad = string.Empty;
        try
        {
            payLoad = jwt.Split('.')[1];
        }
        catch
        {
            throw new InvalidJwtTokenException(nameof(jwt));
        }
        var jsonBytes = ParseBase64WithoutPadding(payLoad);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}