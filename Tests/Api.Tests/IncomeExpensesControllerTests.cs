using Api.Controllers;
using Domain.Entities;
using Infrastructure.Models.Exceptions;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests;

[TestClass]
public class IncomeExpensesControllerTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowExceptionTest()
    {
        var controller = new IncomeExpensesController(null);
    }

    [TestMethod]
    public async Task GetIncomeExpensesSuccessTest()
    {
        var service = new Mock<IIncomeExpenseService>();
        var controller = new IncomeExpensesController(service.Object);

        var result = controller.GetIncomeExpenses();
        
        Assert.AreEqual(typeof(Task<IEnumerable<IncomeExpenseCategory>>), result.GetType());
    }
    
    
    [TestMethod]
    public async Task GetIncomeExpenseSuccessTest()
    {
        var service = new Mock<IIncomeExpenseService>();
        service.Setup((expenseService => expenseService.GetIncomeExpenseType(1)))
            .ReturnsAsync(new IncomeExpenseCategory());
        
        var controller = new IncomeExpensesController(service.Object);

        var result = controller.GetIncomeExpense(1);
        
        Assert.AreEqual(typeof(Task<ActionResult<IncomeExpenseCategory>>), result.GetType());
    }
    
    
    [TestMethod]
    public async Task GetIncomeExpenseBadRequestExceptionTest()
    {
        var service = new Mock<IIncomeExpenseService>();

        var controller = new IncomeExpensesController(service.Object);

        await Assert.ThrowsExceptionAsync<BadRequestException>(() => controller.GetIncomeExpense(-1));
        
    }
    
    [TestMethod]
    public async Task CreateIncomeExpenseTypeSuccessTest()
    {
        var service = new Mock<IIncomeExpenseService>();
        service.Setup((expenseService => expenseService.CreateIncomeExpenseType(new IncomeExpensesAddRequest())))
            .ReturnsAsync(new IncomeExpensesAddResponse());
        
        var controller = new IncomeExpensesController(service.Object);

        var result = controller.CreateIncomeExpenseType(new IncomeExpensesAddRequest());
        
        Assert.AreEqual(typeof(Task<ActionResult<IncomeExpensesAddResponse>>), result.GetType());
    }
    
    [TestMethod]
    public async Task CreateIncomeExpenseTypeBadRequestExceptionTest()
    {
        var service = new Mock<IIncomeExpenseService>();

        var controller = new IncomeExpensesController(service.Object);

        await Assert.ThrowsExceptionAsync<BadRequestException>(() => controller.CreateIncomeExpenseType(null));
    }
    
    
    
    
    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public async Task RemoveIncomeExpenseTypeSuccessTest()
    {
        var service = new Mock<IIncomeExpenseService>();
        
        var controller = new IncomeExpensesController(service.Object);

        await controller.RemoveIncomeExpenseType(-1);
    }
    
    
    [TestMethod]
    public async Task UpdateIncomeExpenseTypeSuccessTest()
    {
        var service = new Mock<IIncomeExpenseService>();
        service.Setup((expenseService => expenseService.UpdateIncomeExpenseType(new IncomeExpensesUpdateRequest())))
            .ReturnsAsync(new IncomeExpensesUpdateResponse());
        
        var controller = new IncomeExpensesController(service.Object);

        var result = controller.UpdateIncomeExpenseType(new IncomeExpensesUpdateRequest());
        
        Assert.AreEqual(typeof(Task<ActionResult<IncomeExpensesUpdateResponse>>), result.GetType());
    }
    
    
    [TestMethod]
    public async Task UpdateIncomeExpenseTypeBadRequestExceptionTest()
    {
        var service = new Mock<IIncomeExpenseService>();

        var controller = new IncomeExpensesController(service.Object);

        await Assert.ThrowsExceptionAsync<BadRequestException>(() => controller.UpdateIncomeExpenseType(null));
    }
}