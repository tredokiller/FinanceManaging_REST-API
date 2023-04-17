using System.Net;
using System.Net.Http.Headers;
using BlazorUI.Services.Interfaces;
using FluentAssertions;
using Moq;
using HttpClientHandler = BlazorUI.Services.HttpClientHandler;

namespace BlazorUI.Tests;

[TestClass]
public class HttpClientHandlerTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowsNullExceptionTest()
    {
        var handler = new HttpClientHandler(null);
    }
    
    [TestMethod]
    public async Task GetAsyncSuccessfulResponse()
    {
        var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent("Hello, world!")
        };

        var httpClientMock = new Mock<IHttpClientHandler>();
        httpClientMock
            .Setup(x => x.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedResponse);

        var httpClient = httpClientMock.Object;
        
        var result = await httpClient.GetAsync("https://example.com/api/data");
        Assert.IsNotNull(result);
        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        var responseContent = await result.Content.ReadAsStringAsync();
        Assert.AreEqual("Hello, world!", responseContent);
    }
    
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void GetAsyncThrowsNullExceptionTest()
    {
        var mockedHttpClient = new Mock<HttpClient>();
        var handler = new HttpClientHandler(mockedHttpClient.Object);

        handler.GetAsync(null);
    }
    
    [TestMethod]
    public async Task GetFromJsonAsyncSuccessTest()
    {
        string expectedResponse = "Hello!";

        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        mockedHttpClient
            .Setup(x => x.GetFromJsonAsync<string>(It.IsAny<string>()))
            .ReturnsAsync(expectedResponse);
        
        var handler = mockedHttpClient.Object;

        var result = await handler.GetFromJsonAsync<string>("response.com/get-something");

        result.Should().Be(expectedResponse);


    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void GetFromJsonAsyncThrowsNullExceptionTest()
    {
        var mockedHttpClient = new Mock<HttpClient>();
        var handler = new HttpClientHandler(mockedHttpClient.Object);

        handler.GetFromJsonAsync<string>(null);
    }
    
    [TestMethod]
    public async Task PostAsJsonAsyncSuccessTest()
    {
        var expectedResponse = new HttpResponseMessage()
        {
            Content = new StringContent("Hello!"),
            StatusCode = HttpStatusCode.Accepted
        };
        
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        mockedHttpClient
            .Setup(x => x.PostAsJsonAsync(It.IsAny<string>() , It.IsAny<string>()))
            .ReturnsAsync(expectedResponse);
        
        var handler = mockedHttpClient.Object;

        var result = await handler.PostAsJsonAsync("asdasd" , "sd");

        result.Content.Should().BeSameAs(expectedResponse.Content);
    }
    
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void PostAsJsonAsyncThrowsNullExceptionTest()
    {
        var mockedHttpClient = new Mock<HttpClient>();
        var handler = new HttpClientHandler(mockedHttpClient.Object);

        handler.PostAsJsonAsync<string>(null , null);
    }
    
    [TestMethod]
    public async Task PuttAsJsonAsyncSuccessTest()
    {
        var expectedResponse = new HttpResponseMessage()
        {
            Content = new StringContent("Hello!"),
            StatusCode = HttpStatusCode.Accepted
        };
        
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        mockedHttpClient
            .Setup(x => x.PutAsJsonAsync(It.IsAny<string>() , It.IsAny<string>()))
            .ReturnsAsync(expectedResponse);
        
        var handler = mockedHttpClient.Object;

        var result = await handler.PutAsJsonAsync("asdasd" , "sd");

        result.Content.Should().BeSameAs(expectedResponse.Content);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void PuttAsJsonAsyncThrowsNullExceptionTest()
    {
        var mockedHttpClient = new Mock<HttpClient>();
        var handler = new HttpClientHandler(mockedHttpClient.Object);

        handler.PutAsJsonAsync<string>(null , null);
    }
    
    [TestMethod]
    public async Task SendAsyncSuccessTest()
    {
        var response = new HttpRequestMessage
        {
            Content = new StringContent("Hello, world!")
        };

        var expectedResponse = new HttpResponseMessage()
        {
            Content = new StringContent("Hello!"),
            StatusCode = HttpStatusCode.Accepted
        };
        
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        mockedHttpClient
            .Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
            .ReturnsAsync(expectedResponse);
        
        var handler = mockedHttpClient.Object;

        var result = await handler.SendAsync(response);

        result.Content.Should().BeSameAs(expectedResponse.Content);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SendAsyncThrowsNullExceptionTest()
    {
        var mockedHttpClient = new Mock<HttpClient>();
        var handler = new HttpClientHandler(mockedHttpClient.Object);

        handler.SendAsync(null);
    }
    
    [TestMethod]
    public void SetAuthorizationSuccessTest()
    {
        var mockedHttpClient = new Mock<HttpClient>();
        var handler = new HttpClientHandler(mockedHttpClient.Object);

        handler.SetAuthorization(new AuthenticationHeaderValue("jwt"));
        
        Assert.AreEqual(handler.GetAuthorizationStatus().GetType() , typeof(AuthenticationHeaderValue));
        Assert.AreEqual(handler.GetAuthorizationStatus().Scheme , "jwt");
        
    }
}