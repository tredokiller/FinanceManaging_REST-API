using Domain.Entities.Enums;

namespace Infrastructure.Models.Responses;

public class IncomeExpensesAddResponse
{
    public int Id { set; get; }
    public string Name { set; get; }
    
    public FinanceActivityEnum FinanceActivityType { set; get; }
}