using Domain.Entities.Enums;

namespace Infrastructure.Models.Responses;

public class IncomeExpensesUpdateResponse
{
    public int Id { set; get; }
    public string Name { set; get; }
    
    public FinanceActivityEnum FinanceActivityType { set; get; }
}