using System.Security.Claims;
using Blazored.LocalStorage;
using BlazorUI.Services;
using BlazorUI.Services.Interfaces;
using Infrastructure.Models.Exceptions;
using Microsoft.AspNetCore.Components.Authorization;
using Moq;

namespace BlazorUI.Tests;

[TestClass]
public class CustomAuthStateProviderTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowsNullExceptionTest()
    {
        var service = new CustomAuthStateProvider(null, null);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidJwtTokenException))]
    public async Task GetAuthenticationStateAsyncInvalidTokenThrowJwtTokenException()
    {
        var localStorageServiceMock = new Mock<ILocalStorageService>();
        var httpClientMock = new Mock<IHttpClientHandler>();

        var token = "valid_token";
        var expectedIdentity = new ClaimsIdentity();
        var expectedUser = new ClaimsPrincipal(expectedIdentity);
        var expectedState = new AuthenticationState(expectedUser);

        localStorageServiceMock.Setup(x => x.GetItemAsStringAsync(It.IsAny<string>() , default))
            .ReturnsAsync(token);
        
//        httpClientMock.SetupProperty(x => x.DefaultRequestHeaders.Authorization, null);
        
        var authenticationStateProvider = new CustomAuthStateProvider(
            localStorageServiceMock.Object, httpClientMock.Object);
        
        var result = await authenticationStateProvider.GetAuthenticationStateAsync();
        
        Assert.AreEqual(expectedState.User.Identity.AuthenticationType, result.User.Identity.AuthenticationType);
        Assert.AreEqual(expectedState.User.Identity.Name, result.User.Identity.Name);
    }

    [TestMethod]
    public async Task GetAuthenticationStateAsyncEmptyTokenReturnsUnauthenticatedState()
    {
        var localStorageServiceMock = new Mock<ILocalStorageService>();
        var httpClientMock = new Mock<IHttpClientHandler>();
        
        var token = string.Empty;
        var expectedIdentity = new ClaimsIdentity();
        var expectedUser = new ClaimsPrincipal(expectedIdentity);
        var expectedState = new AuthenticationState(expectedUser);

        localStorageServiceMock.Setup(x => x.GetItemAsStringAsync(It.IsAny<string>() , default))
            .ReturnsAsync(token);

        var authenticationStateProvider = new CustomAuthStateProvider(
            localStorageServiceMock.Object, httpClientMock.Object);
        var result = await authenticationStateProvider.GetAuthenticationStateAsync();
        
        Assert.AreEqual(expectedState.User.Identity.AuthenticationType, result.User.Identity.AuthenticationType);
        Assert.AreEqual(expectedState.User.Identity.Name, result.User.Identity.Name);
       
        Assert.IsNull(httpClientMock.Object.GetAuthorizationStatus());
    }
    
    
      
    [TestMethod]
    public async Task GetAuthenticationStateAsyncValidTokenReturnsAuthenticatedState()
    {
        var localStorageServiceMock = new Mock<ILocalStorageService>();
        var httpClientMock = new Mock<IHttpClientHandler>();

        var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
        var expectedIdentity = new ClaimsIdentity();
        var expectedUser = new ClaimsPrincipal(expectedIdentity);
        var expectedState = new AuthenticationState(expectedUser);

        localStorageServiceMock.Setup(x => x.GetItemAsStringAsync(It.IsAny<string>() , default))
            .ReturnsAsync(token);
        
//        httpClientMock.SetupProperty(x => x.DefaultRequestHeaders.Authorization, null);
        
        var authenticationStateProvider = new CustomAuthStateProvider(
            localStorageServiceMock.Object, httpClientMock.Object);
        
        var result = await authenticationStateProvider.GetAuthenticationStateAsync();
        
        Assert.AreEqual("jwt", result.User.Identity.AuthenticationType);
        Assert.AreEqual(expectedState.User.Identity.Name, result.User.Identity.Name);
        
        Assert.IsNotNull(httpClientMock.Object.GetAuthorizationStatus());
        Assert.AreEqual("Bearer", httpClientMock.Object.GetAuthorizationStatus().Scheme);
        Assert.AreEqual(token.Replace("\"", ""), httpClientMock.Object.GetAuthorizationStatus().Parameter);
    }
}