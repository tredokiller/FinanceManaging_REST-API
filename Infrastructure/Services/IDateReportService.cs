using Domain.Entities;

namespace Infrastructure.Services;

public interface IDateReportService
{
    Task<DateReport> GetDailyReport(DateTime data);
    Task<DateReport> GetDatePeriodReport(DateTime startDate , DateTime endDate);
}