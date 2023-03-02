using Infrastructure.Models;
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
    
    /// <response code="200">Successful Response</response>
    /// <response code="400">Invalid Date</response>
    [HttpGet("GetDailyReport")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<DateReport> GetDailyReport(DateTime date)
    {
        return _dateReportService.GetDailyReport(date);
    }
    
    /// <response code="200">Successful Response</response>
    /// <response code="400">Invalid Date</response>
    [HttpGet("GetDatePeriodReport")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<DateReport> GetDatePeriodReport(DateTime startDate, DateTime endDate)
    {
        return _dateReportService.GetDatePeriodReport(startDate , endDate);
    }
}