using Infrastructure.Models;

namespace Infrastructure.Services;

public interface IDateReportService
{
    Task<DateReport> GetDailyReport(DateTime date);
    Task<DateReport> GetPeriodOfDatesReport(DateTime startDate , DateTime endDate);
}