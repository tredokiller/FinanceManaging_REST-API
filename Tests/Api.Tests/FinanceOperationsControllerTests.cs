using Api.Controllers;
using Domain.Entities;
using Infrastructure.Models.Exceptions;
using Infrastructure.Models.Requests;
using Infrastructure.Models.Responses;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests;

[TestClass]
public class FinanceOperationsControllerTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowExceptionTest()
    {
        var controller = new FinanceOperationsController(null);
    }


    [TestMethod]
    public async Task GetFinanceOperationBadRequestExceptionTest()
    {
        var service = new Mock<IFinanceOperationService>();
        var controller = new FinanceOperationsController(service.Object);

        await Assert.ThrowsExceptionAsync<BadRequestException>(() => controller.GetFinanceOperation(-1));
    }
    
    [TestMethod]
    public async Task GetFinanceOperationSuccessTest()
    {
        var service = new Mock<IFinanceOperationService>();

        service.Setup((operationService => operationService.GetFinanceOperation(1))).ReturnsAsync(new FinanceOperation());
        var controller = new FinanceOperationsController(service.Object);

        var result = await controller.GetFinanceOperation(1);
        
        Assert.AreEqual(typeof(ActionResult<FinanceOperation>), result.GetType());
    }


    [TestMethod]
    public async Task CreateFinanceOperationBadRequestExceptionTest()
    {
        var service = new Mock<IFinanceOperationService>();
        var controller = new FinanceOperationsController(service.Object);

        await Assert.ThrowsExceptionAsync<BadRequestException>(() => controller.CreateFinanceOperation(null));
    }
    
    [TestMethod]
    public async Task CreateFinanceOperationSuccessTest()
    {
        var service = new Mock<IFinanceOperationService>();

        service.Setup((operationService => operationService.CreateFinanceOperation(new FinanceOperationAddRequest()))).ReturnsAsync(new FinanceOperationAddResponse());
        var controller = new FinanceOperationsController(service.Object);

        var result = await controller.CreateFinanceOperation(new FinanceOperationAddRequest());
        
        Assert.AreEqual(typeof(ActionResult<FinanceOperationAddResponse>), result.GetType());
    }
    
    
    [TestMethod]
    [ExpectedException(typeof(BadRequestException))]
    public void  RemoveFinanceOperationBadRequestExceptionTest()
    {
        var service = new Mock<IFinanceOperationService>();
        var controller = new FinanceOperationsController(service.Object);
        
        controller.RemoveFinanceOperation(-1);
    }
    
    
    [TestMethod]
    public async Task  UpdateFinanceOperationBadRequestExceptionTest()
    {
        var service = new Mock<IFinanceOperationService>();
        var controller = new FinanceOperationsController(service.Object);
        
        await Assert.ThrowsExceptionAsync<BadRequestException>(() => controller.UpdateFinanceOperation(null));
    }
    
    
    [TestMethod]
    public async Task UpdateFinanceOperationSuccessTest()
    {
        var service = new Mock<IFinanceOperationService>();

        service.Setup((operationService => operationService.UpdateFinanceOperation(new FinanceOperationUpdateRequest()))).ReturnsAsync(new FinanceOperationUpdateResponse());
        var controller = new FinanceOperationsController(service.Object);

        var result = await controller.UpdateFinanceOperation(new FinanceOperationUpdateRequest());
        
        Assert.AreEqual(typeof(ActionResult<FinanceOperationUpdateResponse>), result.GetType());
    }
}