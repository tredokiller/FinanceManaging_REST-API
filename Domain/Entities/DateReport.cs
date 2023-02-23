namespace Domain.Entities;

public class DateReport
{
    public float TotalIncome { set; get; }
    public float TotalExpense { set; get; }
    
    public List<FinanceOperation>? Operations { set; get; }

}