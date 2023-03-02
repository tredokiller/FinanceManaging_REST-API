using Api.Controllers;
using Infrastructure.Models;
using Infrastructure.Models.Requests;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests;

[TestClass]
public class AuthenticationControllerTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowExceptionTest()
    {
        var controller = new AuthenticationController(null);
    }
    
    [TestMethod]
    [ExpectedException(typeof(BadHttpRequestException))]
    public void AuthenticateThrowBadRequestExceptionTest()
    {
        var service = new Mock<IUserService>();
        var controller = new AuthenticationController(service.Object);

        controller.Authenticate(null);
    }
    
    [TestMethod]
    public void AuthenticateSuccessExceptionTest()
    {
        var service = new Mock<IUserService>();

        var request = new UserAuthenticateRequest();
        
        service.Setup((operationService => operationService.Authenticate(request))).ReturnsAsync(new UserToken());
        var controller = new AuthenticationController(service.Object);

        var result = controller.Authenticate(request);
        
        Assert.AreEqual(typeof(OkObjectResult), result.GetType());
    }
    
    [TestMethod]
    public void AuthenticateThrowUnAuthorizedTest()
    {
        var service = new Mock<IUserService>();
        var controller = new AuthenticationController(service.Object);

        var result = controller.Authenticate(new UserAuthenticateRequest());
        
        Assert.AreEqual(typeof(UnauthorizedResult), result.GetType());
    }
}