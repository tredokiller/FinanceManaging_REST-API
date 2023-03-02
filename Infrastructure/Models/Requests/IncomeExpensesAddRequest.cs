using Domain.Entities.Enums;

namespace Infrastructure.Models.Requests;

public class IncomeExpensesAddRequest
{ 
    public string Name { set; get; }
    
    public FinanceActivityEnum FinanceActivityType { set; get; }
}