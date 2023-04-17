using BlazorUI.Services;
using BlazorUI.Services.Interfaces;
using Domain.Entities;
using Infrastructure.Models;
using Moq;
using Newtonsoft.Json;

namespace BlazorUI.Tests;

[TestClass]
public class DataReportServiceTests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ConstructorThrowsNullExceptionTest()
    {
        var service = new DateReportService(null);
    }

    [TestMethod]
    public async Task GetDateReportSuccess()
    {
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        DateReport report = new DateReport()
        {
            TotalExpense = 10f,
            TotalIncome = 15f,
            Operations = new List<FinanceOperation>()
        };

        HttpResponseMessage testResponse = new HttpResponseMessage();

        testResponse.Content = new StringContent(JsonConvert.SerializeObject(report));

        mockedHttpClient.Setup(handler => handler.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(testResponse);

        var service = new DateReportService(mockedHttpClient.Object);
        var result = await service.GetDailyReport(new DateTime());
        
        Assert.AreEqual(report.TotalExpense , result.TotalExpense);
        Assert.AreEqual(report.TotalIncome , result.TotalIncome);
    }
    
    
    [TestMethod]
    public async Task GetPeriodOfDatesReportSuccess()
    {
        var mockedHttpClient = new Mock<IHttpClientHandler>();
        
        DateReport report = new DateReport()
        {
            TotalExpense = 200f,
            TotalIncome = 150f,
            Operations = new List<FinanceOperation>()
        };

        HttpResponseMessage testResponse = new HttpResponseMessage();

        testResponse.Content = new StringContent(JsonConvert.SerializeObject(report));

        mockedHttpClient.Setup(handler => handler.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(testResponse);

        var service = new DateReportService(mockedHttpClient.Object);
        var result = await service.GetPeriodOfDatesReport(new DateTime() , new DateTime());
        
        Assert.AreEqual(report.TotalExpense , result.TotalExpense);
        Assert.AreEqual(report.TotalIncome , result.TotalIncome);
    }
    
}