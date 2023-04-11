using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
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
    /// <summary>
    /// Get Daily Report
    /// </summary>
    /// <remarks>
    /// Data example
    ///
    ///     {
    ///        "totalIncome": 0,
    ///         "totalExpense": 0,
    ///         "operations": [
    ///         {
    ///             "id": 0,
    ///             "type": "string",
    ///             "category": "string",
    ///             "categoryId": 0,
    ///             "amount": 0,
    ///             "date": "2023-03-04T07:16:35.339Z"
    ///         }
    ///         ]
    ///     }
    /// 
    /// </remarks>
    [AllowAnonymous]
    [HttpGet("GetDailyReport")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<DateReport> GetDailyReport(DateTime date)
    {
        return _dateReportService.GetDailyReport(date);
    }
    
    /// <response code="200">Successful Response</response>
    /// <response code="400">Invalid Date</response>
    /// <summary>
    /// Get Data Period Report
    /// </summary>
    /// <remarks>
    /// Period of dates
    /// </remarks>
    [HttpGet("GetDatePeriodReport")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<DateReport> GetDatePeriodReport(DateTime startDate, DateTime endDate)
    {
        return _dateReportService.GetPeriodOfDatesReport(startDate , endDate);
    }
}