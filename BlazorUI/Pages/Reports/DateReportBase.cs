using Infrastructure.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Pages.Reports;

public class DateReportBase: ComponentBase
{
    [Inject] 
    protected IDateReportService DateReportService { set; get; }
    
    [Inject] 
    protected NavigationManager NavigationManager { set; get; }
    
    protected static DateTime FirstDate { set; get; }
    protected static DateTime SecondDate { set; get; }
    
    protected void ToDateReportMain()
    {
        NavigationManager.NavigateTo("/date-report");
    }
    
    
    protected override Task OnInitializedAsync()
    {
        return base.OnInitializedAsync();
    }
}