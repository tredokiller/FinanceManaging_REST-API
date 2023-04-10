using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorUI.Pages.IncomeExpenses;

public class IncomeExpensesBase : ComponentBase
{
    [Inject]
    protected IIncomeExpenseService IncomeExpenseService { get; set; }


    [Inject] 
    protected NavigationManager NavigationManager { get; set; }

    protected IEnumerable<IncomeExpenseCategory> IncomeExpenseCategories { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadCategories();
    }

    private async Task LoadCategories()
    {
        IncomeExpenseCategories = (await IncomeExpenseService.GetIncomeExpenseTypes()).ToList();
    }

    protected void ToCategories()
    {
        NavigationManager.NavigateTo("/income-expense-categories");
    }
    
    
    protected void CreateNewCategory()
    {
        NavigationManager.NavigateTo("/income-expense-categories/new");
    }

    protected void EditCategory(IncomeExpenseCategory category)
    {
        NavigationManager.NavigateTo($"/income-expense-categories/edit/{category.Id}");
    }
    
    protected Task DeleteCategory(IncomeExpenseCategory category)
    {
        NavigationManager.NavigateTo($"/income-expense-categories/delete/{category.Id}");
        return Task.CompletedTask;
    }
    
}