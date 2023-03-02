namespace Infrastructure.Models.Requests;

public class FinanceOperationUpdateRequest
{
    public int Id { set; get; }
    public int Category { set; get; }
    public float Amount { set; get; }
    
    public DateTime Date { set; get; }
}