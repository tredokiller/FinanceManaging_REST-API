using System.Net.Http.Headers;

namespace BlazorUI.Services.Interfaces;

public interface IHttpClientHandler
{
    public Task<HttpResponseMessage> GetAsync(string requestUri);
    public Task<TValue?> GetFromJsonAsync <TValue> (string requestUri);
    public Task<HttpResponseMessage> PostAsJsonAsync <TValue> (string requestUri , TValue value);
    public Task<HttpResponseMessage> PutAsJsonAsync <TValue> (string requestUri , TValue value);
    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);

    public void SetAuthorization(AuthenticationHeaderValue? authenticationHeaderValue);
    public AuthenticationHeaderValue? GetAuthorizationStatus();
}