using Domain.Entities.Enums;

namespace Infrastructure.Models.Responses;

public class IncomeExpensesGetResponse
{
    public string Id { set; get; }
    public string Name { get; set; }
    
    public FinanceActivityEnum FinanceActivityType { set; get; }
}