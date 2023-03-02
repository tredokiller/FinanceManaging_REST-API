using Api.Controllers;
using Infrastructure.Models;
using Infrastructure.Models.Requests;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Api.Tests;

[TestClass]
public class DateReportControllerTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowExceptionTest()
    {
        var controller = new DateReportController(null);
    }

    [TestMethod]
    public async Task GetDailyReportSuccessTest()
    {
        var service = new Mock<IDateReportService>();

        service.Setup((reportService => reportService.GetDailyReport(new DateTime()))).ReturnsAsync(new DateReport());
        var controller = new DateReportController(service.Object);

        var result = controller.GetDailyReport(new DateTime());
        
        Assert.AreEqual(typeof(Task<DateReport>), result.GetType());
    }
    
    [TestMethod]
    public async Task GetDatePeriodReportSuccessTest()
    {
        var service = new Mock<IDateReportService>();

        service.Setup((reportService => reportService.GetDatePeriodReport(new DateTime() , new DateTime()))).ReturnsAsync(new DateReport());
        var controller = new DateReportController(service.Object);

        var result = controller.GetDailyReport(new DateTime());
        
        Assert.AreEqual(typeof(Task<DateReport>), result.GetType());
    }
}