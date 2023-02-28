using Domain.Entities.Enums;

namespace Domain.Entities;

public class IncomeExpenseCategory : BaseEntity
{ 
    public string Name { get; set; }

    public FinanceActivityEnum FinanceActivityType { set; get; }
}