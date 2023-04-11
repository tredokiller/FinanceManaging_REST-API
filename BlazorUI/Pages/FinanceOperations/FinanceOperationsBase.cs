using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Pages.FinanceOperations;

[Authorize]
public class FinanceOperationsBase : ComponentBase
{
    [Inject]
    protected IFinanceOperationService FinanceOperationsService { get; set; }
    
    [Inject]
    protected IIncomeExpenseService IncomeExpenseService { get; set; }
    
    [Inject] 
    protected NavigationManager NavigationManager { get; set; }

    protected IEnumerable<FinanceOperation> FinanceOperations { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadOperations();
    }

    private async Task LoadOperations()
    {
        FinanceOperations = (await FinanceOperationsService.GetFinanceOperations()).ToList();
    }

    protected void ToOperations()
    {
        NavigationManager.NavigateTo("/finance-operations");
    }
    
    protected void CreateNewFinanceOperation()
    {
        NavigationManager.NavigateTo("/finance-operations/new");
    }

    protected void EditOperation(FinanceOperation category)
    {
        NavigationManager.NavigateTo($"/finance-operations/edit/{category.Id}");
    }
    
    protected void DeleteOperation(FinanceOperation category)
    {
        NavigationManager.NavigateTo($"/finance-operations/delete/{category.Id}");
    }

}