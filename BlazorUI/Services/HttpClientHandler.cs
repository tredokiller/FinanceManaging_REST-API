using System.Net.Http.Headers;
using BlazorUI.Services.Interfaces;

namespace BlazorUI.Services;

public class HttpClientHandler : IHttpClientHandler
{
    private readonly HttpClient _httpClient;
    
    public HttpClientHandler(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
    
    public Task<HttpResponseMessage> GetAsync(string requestUri)
    {
        if (requestUri == null)
        {
            throw new ArgumentNullException(nameof(requestUri));
        }
        
        return _httpClient.GetAsync(requestUri);
    }

    public Task<TValue?> GetFromJsonAsync<TValue>(string requestUri)
    {
        if (requestUri == null)
        {
            throw new ArgumentNullException(nameof(requestUri));
        }
        
        return _httpClient.GetFromJsonAsync<TValue>(requestUri);
    }

    public Task<HttpResponseMessage> PostAsJsonAsync<TValue>(string requestUri, TValue value)
    {
        if (requestUri == null)
        {
            throw new ArgumentNullException(nameof(requestUri));
        }
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value)); 
        }
        
        return _httpClient.PostAsJsonAsync(requestUri, value);
    }

    public Task<HttpResponseMessage> PutAsJsonAsync<TValue>(string requestUri, TValue value)
    {
        if (requestUri == null)
        {
            throw new ArgumentNullException(nameof(requestUri));
        }
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value)); 
        }
        
        return _httpClient.PutAsJsonAsync(requestUri, value);
    }

    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        
        return _httpClient.SendAsync(request);
    }

    public void SetAuthorization(AuthenticationHeaderValue? authenticationHeaderValue)
    {
        _httpClient.DefaultRequestHeaders.Authorization = authenticationHeaderValue;
    }

    public AuthenticationHeaderValue? GetAuthorizationStatus()
    {
        return _httpClient.DefaultRequestHeaders.Authorization;
    }
}