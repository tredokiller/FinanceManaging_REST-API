using BlazorUI.Services.Interfaces;
using Infrastructure.Models;
using Infrastructure.Services;
using Newtonsoft.Json;

namespace BlazorUI.Services;

public class DateReportService : IDateReportService
{
    private readonly IHttpClientHandler _httpClient;

    public DateReportService(IHttpClientHandler client)
    {
        _httpClient = client ?? throw new ArgumentNullException(nameof(client));
    }
    public async Task<DateReport> GetDailyReport(DateTime date)
    {
        var dateString = date.ToString("MM.dd.yyyy");
        var url = $"api/DateReport/GetDailyReport?date={dateString}";
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DateReport>(responseContent);
        }

        return null!;
    }

    public async Task<DateReport> GetPeriodOfDatesReport(DateTime startDate, DateTime endDate)
    {
        var startDateString = startDate.ToString("MM.dd.yyyy");
        var finalDateString = endDate.ToString("MM.dd.yyyy");
        var url = $"api/DateReport/GetDatePeriodReport?startDate={startDateString}&endDate={finalDateString}";
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DateReport>(responseContent);
        }

        return null!;
    }
}