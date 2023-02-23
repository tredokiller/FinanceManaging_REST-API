namespace Domain.Entities;

public class FinanceOperation : BaseEntity
{
    public string Type { set; get; }
    
    public string Category { set; get; }
    public int CategoryId { set; get; }
    public float Amount { set; get; }
    
    public DateTime Date { set; get; }

    public IncomeExpenseCategory CategoryType;
}