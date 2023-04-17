using System.Net;
using System.Net.Http.Json;
using BlazorUI.Services;
using BlazorUI.Services.Interfaces;
using Domain.Entities;
using Domain.Entities.Enums;
using FluentAssertions;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;
using Moq;

namespace BlazorUI.Tests;

[TestClass]
public class IncomeExpensesServiceTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowsNullExceptionTest()
    {
        var service = new IncomeExpensesService(null);
    }
    
    [TestMethod]
    public async Task GetIncomeExpensesSuccessTest()
    {
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        var service = new IncomeExpensesService(mockedHttpClient.Object);


        IEnumerable<IncomeExpenseCategory> incomeExpenseCategories = new[]
        {
            new IncomeExpenseCategory()
            {
                Id = 1,
                FinanceActivityType = FinanceActivityEnum.Income,
                Name = "Income"
            },
            new IncomeExpenseCategory()
            {
                Id = 2,
                FinanceActivityType = FinanceActivityEnum.Expense,
                Name = "Expense"
            }
        };
        
          
        

        mockedHttpClient.Setup(handler => handler.GetFromJsonAsync<IncomeExpenseCategory[]>(It.IsAny<string>()))
            .ReturnsAsync(incomeExpenseCategories.ToArray);
        
        var result = await service.GetIncomeExpenseTypes();
        
        result.Should().BeOfType<IncomeExpenseCategory[]>();
        result.Count().Should().Be(incomeExpenseCategories.Count());
    }
    
    
    [TestMethod]
    public async Task GetIncomeExpenseCategorySuccessTest()
    {
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        var service = new IncomeExpensesService(mockedHttpClient.Object);
        
        var incomeExpenseCategory = new IncomeExpenseCategory()
        {
           Id = 1,
           FinanceActivityType = FinanceActivityEnum.Income,
           Name = "Income"
        };

        var response = new HttpResponseMessage();
        
        response.Content = JsonContent.Create(incomeExpenseCategory);
        
        mockedHttpClient.Setup(handler => handler.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(response);
        
        IncomeExpenseCategory result = await service.GetIncomeExpenseType(1);
        
        result.Should().BeOfType<IncomeExpenseCategory>();
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("Income");
        result.FinanceActivityType.Should().Be(FinanceActivityEnum.Income);
    }
    
    [TestMethod]
    [ExpectedException(typeof(HttpRequestException))]
    public async Task GetIncomeExpenseCategoryThrowsHttpRequestExceptionTest()
    {
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        var service = new IncomeExpensesService(mockedHttpClient.Object);
        
        mockedHttpClient.Setup(handler => handler.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));
        
        IncomeExpenseCategory result = await service.GetIncomeExpenseType(1);
    }
    
    [TestMethod]
    public async Task CreateIncomeExpenseCategorySuccessTest()
    {
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        var service = new IncomeExpensesService(mockedHttpClient.Object);
        
        var incomeExpensesAddRequest = new IncomeExpensesAddRequest()
        {
            FinanceActivityType = FinanceActivityEnum.Income,
            Name = "Income"
        };

        var response = new HttpResponseMessage();
        
        response.Content = JsonContent.Create(incomeExpensesAddRequest);
        
        mockedHttpClient.Setup(handler => handler.PostAsJsonAsync(It.IsAny<string>() , incomeExpensesAddRequest))
            .ReturnsAsync(response);
        
        IncomeExpensesAddResponse result = await service.CreateIncomeExpenseType(incomeExpensesAddRequest);
        
        result.Should().BeOfType<IncomeExpensesAddResponse>();
        
        result.FinanceActivityType.Should().Be(FinanceActivityEnum.Income);
        result.Name.Should().Be("Income");
        
        result.Should().NotBeNull();
    }


    [TestMethod]
    [ExpectedException(typeof(HttpRequestException))]
    public async Task CreateIncomeExpenseCategoryThrowsNullExceptionTest()
    {
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        var service = new IncomeExpensesService(mockedHttpClient.Object);

        mockedHttpClient.Setup(handler => handler.PostAsJsonAsync(It.IsAny<string>(), It.IsAny<IncomeExpensesAddRequest>()))
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));
        
        IncomeExpensesAddResponse result = await service.CreateIncomeExpenseType(new IncomeExpensesAddRequest());
    }
    
    [TestMethod]
    [ExpectedException(typeof(HttpRequestException))]
    public async Task RemoveIncomeExpenseCategoryThrowsHttpRequestExceptionTest()
    {
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        var service = new IncomeExpensesService(mockedHttpClient.Object);
        
        mockedHttpClient.Setup(handler => handler.SendAsync(It.IsAny<HttpRequestMessage>()))
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));
        
        await service.RemoveIncomeExpenseType(1);
    }
    
    [TestMethod]
    public async Task UpdateIncomeExpenseCategorySuccessTest()
    {
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        var service = new IncomeExpensesService(mockedHttpClient.Object);
        
        var incomeExpenseUpdateRequest = new IncomeExpensesUpdateRequest()
        {
            Id = 1,
            FinanceActivityType = FinanceActivityEnum.Income,
            Name = "icome"
        };
        
        var updatedIncomeExpenseUpdateRequest = new IncomeExpensesUpdateRequest()
        {
            Id = 1,
            FinanceActivityType = FinanceActivityEnum.Income,
            Name = "Income"
        };

        var response = new HttpResponseMessage();
        
        response.Content = JsonContent.Create(updatedIncomeExpenseUpdateRequest);
        
        mockedHttpClient.Setup(handler => handler.PutAsJsonAsync(It.IsAny<string>() , incomeExpenseUpdateRequest))
            .ReturnsAsync(response);
        
        IncomeExpensesUpdateResponse result = await service.UpdateIncomeExpenseType(incomeExpenseUpdateRequest);
        
        result.Should().BeOfType<IncomeExpensesUpdateResponse>();
        result.Name.Should().Be("Income");
        
        result.FinanceActivityType.Should().Be(FinanceActivityEnum.Income);
        result.Id.Should().Be(1);
        
        result.Should().NotBeNull();
        
    }
    
    [TestMethod]
    [ExpectedException(typeof(HttpRequestException))]
    public async Task UpdateFinanceOperationThrowsNullExceptionTest()
    {
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        var service = new IncomeExpensesService(mockedHttpClient.Object);

        mockedHttpClient.Setup(handler => handler.PutAsJsonAsync(It.IsAny<string>(), It.IsAny<IncomeExpensesUpdateRequest>()))
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));
        
        IncomeExpensesUpdateResponse result = await service.UpdateIncomeExpenseType(new IncomeExpensesUpdateRequest());
    }
    
}