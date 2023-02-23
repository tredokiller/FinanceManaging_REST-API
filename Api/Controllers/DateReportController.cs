using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DateReportController : ControllerBase
{
    private readonly IDateReportService _dateReportService;
    
    public DateReportController(IDateReportService dateReportService)
    {
        _dateReportService = dateReportService ?? throw new ArgumentNullException(nameof(dateReportService));
    }


    [HttpGet("GetDailyReport")]
    public Task<DateReport> GetDailyReport(DateTime date)
    {
        return _dateReportService.GetDailyReport(date);
    }
    
    
    [HttpGet("GetDatePeriodReport")]
    public Task<DateReport> GetDatePeriodReport(DateTime startDate, DateTime endDate)
    {
        return _dateReportService.GetDatePeriodReport(startDate , endDate);
    }
}