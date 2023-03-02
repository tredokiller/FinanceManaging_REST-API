using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Repository;
using FluentAssertions;
using FluentAssertions.Common;
using Infrastructure.Models;
using Infrastructure.Models.Exceptions;
using Infrastructure.Models.Requests;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Infrastructure.Tests;

[TestClass]
public class UserServiceTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowArgumentNullExceptionTest()
    {
        var service = new UserService(null, null);
    }
    
    [TestMethod]
    public async Task AuthenticateSuccessReturnsToken()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "JWT:Key", "my-super-duper-secret-key" },
            }!)
            .Build();
        var users = new List<User>
        {
            new User { Name = "testuser", Password = "password" },
        };
        var usersRepositoryMock = new Mock<IRepository<User>>();
        usersRepositoryMock.Setup(x => x.GetAll().Result).Returns(users);

        var service = new UserService(usersRepositoryMock.Object, configuration);
        
        var result = await service.Authenticate(new UserAuthenticateRequest
        {
            Name = "testuser",
            Password = "password",
        });
        
        Assert.IsNotNull(result);
        Assert.IsInstanceOfType(result, typeof(UserToken));
        Assert.IsFalse(string.IsNullOrWhiteSpace(result.Token));
    }


    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task AuthenticateReturnsBadRequest()
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "JWT:Key", "my-super-duper-secret-key" },
            }!)
            .Build();

        var usersRepositoryMock = new Mock<IRepository<User>>();
        var service = new UserService(usersRepositoryMock.Object, configuration);

        service.Authenticate(null);
    }
}