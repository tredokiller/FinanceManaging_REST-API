using System.Net;
using System.Net.Http.Json;
using BlazorUI.Services;
using BlazorUI.Services.Interfaces;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;
using Moq;

namespace BlazorUI.Tests;

[TestClass]
public class FinanceOperationServiceTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowsNullExceptionTest()
    {
        var service = new FinanceOperationService(null);
    }
    
    [TestMethod]
    public async Task GetFinanceOperationsSuccessTest()
    {
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        var service = new FinanceOperationService(mockedHttpClient.Object);


        IEnumerable<FinanceOperation> financeOperations = new []
        {
            new FinanceOperation()
            {
                Date = new DateTime(),
                Amount = 100,
                Category = "Deposit",
                CategoryId = 1,
                CategoryType = new IncomeExpenseCategory(),
                Id = 1,
                Type = "Income"
            },
            new FinanceOperation()
            {
                Date = new DateTime(),
                Amount = 10,
                Category = "Rent Car",
                CategoryId = 1,
                CategoryType = new IncomeExpenseCategory(),
                Id = 1,
                Type = "Expense"
            }
        };

        mockedHttpClient.Setup(handler => handler.GetFromJsonAsync<FinanceOperation[]>(It.IsAny<string>()))
            .ReturnsAsync(financeOperations.ToArray);
        
        var result = await service.GetFinanceOperations();
        
        result.Should().BeOfType<FinanceOperation[]>();
        result.Count().Should().Be(financeOperations.Count());
    }
    
    
    [TestMethod]
    public async Task GetFinanceOperationSuccessTest()
    {
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        var service = new FinanceOperationService(mockedHttpClient.Object);
        
        var financeOperation = new FinanceOperation()
        {
            Date = new DateTime(),
            Amount = 10,
            Category = "Rent Car",
            CategoryId = 1,
            CategoryType = null,
            Id = 1,
            Type = "Expense"
        };

        var response = new HttpResponseMessage();
        
        response.Content = JsonContent.Create(financeOperation);
        
        mockedHttpClient.Setup(handler => handler.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(response);

        
        
        FinanceOperation result = service.GetFinanceOperation(1).Result;
        
        result.Should().BeOfType<FinanceOperation>();
        result.Should().NotBeNull();
        result.Amount.Should().Be(10F);
        result.Category.Should().Be("Rent Car");
        result.CategoryId.Should().Be(1);
        result.CategoryType.Should().BeNull();
        result.Date.Should().Be(new DateTime());
        result.Id.Should().Be(1);
        result.Type.Should().Be("Expense");
    }
    
    [TestMethod]
    [ExpectedException(typeof(HttpRequestException))]
    public async Task GetFinanceOperationThrowsHttpRequestExceptionTest()
    {
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        var service = new FinanceOperationService(mockedHttpClient.Object);
        

        mockedHttpClient.Setup(handler => handler.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));
        
        FinanceOperation result = await service.GetFinanceOperation(1);
    }
    
    [TestMethod]
    public async Task CreateFinanceOperationSuccessTest()
    {
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        var service = new FinanceOperationService(mockedHttpClient.Object);
        
        var financeOperationAddRequest = new FinanceOperationAddRequest()
        {
            Date = new DateTime(),
            Amount = 10,
            CategoryId = 1
        };

        var response = new HttpResponseMessage();
        
        response.Content = JsonContent.Create(financeOperationAddRequest);
        
        mockedHttpClient.Setup(handler => handler.PostAsJsonAsync(It.IsAny<string>() , financeOperationAddRequest))
            .ReturnsAsync(response);

        
        
        FinanceOperationAddResponse result = await service.CreateFinanceOperation(financeOperationAddRequest);
        
        result.Should().BeOfType<FinanceOperationAddResponse>();
        result.Should().NotBeNull();
        result.Amount.Should().Be(10F);
        result.Date.Should().Be(new DateTime());
    }


    [TestMethod]
    [ExpectedException(typeof(HttpRequestException))]
    public async Task CreateFinanceOperationThrowsNullExceptionTest()
    {
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        var service = new FinanceOperationService(mockedHttpClient.Object);

        mockedHttpClient.Setup(handler => handler.PostAsJsonAsync(It.IsAny<string>(), It.IsAny<FinanceOperationAddRequest>()))
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));
        
        FinanceOperationAddResponse result = await service.CreateFinanceOperation(new FinanceOperationAddRequest());
    }
    
    [TestMethod]
    [ExpectedException(typeof(HttpRequestException))]
    public async Task RemoveFinanceOperationThrowsHttpRequestExceptionTest()
    {
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        var service = new FinanceOperationService(mockedHttpClient.Object);
        
        mockedHttpClient.Setup(handler => handler.SendAsync(It.IsAny<HttpRequestMessage>()))
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));
        
        await service.RemoveFinanceOperation(1);
        
    }
    
    [TestMethod]
    public async Task UpdateFinanceOperationSuccessTest()
    {
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        var service = new FinanceOperationService(mockedHttpClient.Object);
        
        var financeOperationUpdateRequest = new FinanceOperationUpdateRequest()
        {
            Date = new DateTime(),
            Amount = 10,
            CategoryId = 1,
            Id = 1
        };
        
        var updatedFinanceOperationUpdateRequest = new FinanceOperationUpdateRequest()
        {
            Date = new DateTime(),
            Amount = 100,
            CategoryId = 2,
            Id = 1
        };

        var response = new HttpResponseMessage();
        
        response.Content = JsonContent.Create(updatedFinanceOperationUpdateRequest);
        
        mockedHttpClient.Setup(handler => handler.PutAsJsonAsync(It.IsAny<string>() , financeOperationUpdateRequest))
            .ReturnsAsync(response);

        
        
        FinanceOperationUpdateResponse result = await service.UpdateFinanceOperation(financeOperationUpdateRequest);
        
        result.Should().BeOfType<FinanceOperationUpdateResponse>();
        result.Should().NotBeNull();
        result.Amount.Should().Be(100F);
        result.CategoryId.Should().Be(2);
        
        result.Date.Should().Be(new DateTime());
    }
    
    [TestMethod]
    [ExpectedException(typeof(HttpRequestException))]
    public async Task UpdateFinanceOperationThrowsNullExceptionTest()
    {
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        var service = new FinanceOperationService(mockedHttpClient.Object);

        mockedHttpClient.Setup(handler => handler.PutAsJsonAsync(It.IsAny<string>(), It.IsAny<FinanceOperationUpdateRequest>()))
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));
        
        FinanceOperationUpdateResponse result = await service.UpdateFinanceOperation(new FinanceOperationUpdateRequest());
    }
    
}