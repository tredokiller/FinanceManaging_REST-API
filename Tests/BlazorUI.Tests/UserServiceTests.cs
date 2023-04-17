using System.Net;
using System.Net.Http.Json;
using BlazorUI.Services;
using BlazorUI.Services.Interfaces;
using FluentAssertions;
using Infrastructure.Models;
using Infrastructure.Models.Requests;
using Moq;

namespace BlazorUI.Tests;

[TestClass]
public class UserServiceTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowsNullExceptionTest()
    {
        var service = new UserService(null);
    }

    [TestMethod]
    public async Task AuthenticationSuccessTest()
    {
        var httpHandlerMock = new Mock<IHttpClientHandler>();

        var userAuthenticateRequest = new UserAuthenticateRequest()
        {
            Name = "Bobik",
            Password = "Ravlik228"
        };
        
        var userAuthenticationToken= Task.FromResult(new UserToken { Token = "1234erwf234w4134r23fesDVC12341ree@2345rewfW", RefreshToken = null});
        var response = new HttpResponseMessage();
        
        response.Content = JsonContent.Create(userAuthenticationToken);
        
        httpHandlerMock.Setup( handler => handler.PostAsJsonAsync(It.IsAny<string>() , userAuthenticateRequest))
            .ReturnsAsync(response);
        
        var service = new UserService(httpHandlerMock.Object);

        var result = await service.Authenticate(userAuthenticateRequest);

        result.Should().BeOfType<UserToken>();
    }
    
    [TestMethod]
    [ExpectedException(typeof(HttpRequestException))]
    public async Task AuthenticationThrowsHttpRequestExceptionTest()
    {
        var httpHandlerMock = new Mock<IHttpClientHandler>();

        httpHandlerMock.Setup( handler => handler.PostAsJsonAsync(It.IsAny<string>() , It.IsAny<UserAuthenticateRequest>()))
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));
        
        var service = new UserService(httpHandlerMock.Object);

        var result = await service.Authenticate(new UserAuthenticateRequest());
    }
}