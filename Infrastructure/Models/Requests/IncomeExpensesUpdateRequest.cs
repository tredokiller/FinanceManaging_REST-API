using Domain.Entities.Enums;

namespace Infrastructure.Models.Requests;

public class IncomeExpensesUpdateRequest
{
    public int Id { set; get; }
    public string Name { set; get; }
    
    public FinanceActivityEnum FinanceActivityType { set; get; }
}